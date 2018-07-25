using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Library.Common;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions.Rent;
using Library.Services.Services;

namespace Library.Services.Impls.Services
{
	public class RentsService : IRentsService
	{
		private readonly IUnitOfWork _unitOfWork;
		private static readonly string IncludeProps = $"{nameof(Rent.Subscriber)}, {nameof(Rent.Book)}";
		public RentsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<long> Count()
		{
			return await _unitOfWork.RentRepository.Count();
		}
		public async Task<IEnumerable<RentDto>> GetAll(PagingParameterModel pagingParameterModel)
		{
			var orderBy = Helper.GetOrder<Rent>(pagingParameterModel, x =>
			{
				switch (x.Name)
				{
					case nameof(Rent.Date):
						return r => r.Date.ToString();
					case nameof(Rent.Count):
						return r => r.Count.ToString();
					case "Active":
						return r => r.IsActive.ToString();
					case nameof(Book):
						return r => r.Book.Name;
					case nameof(Subscriber):
						return r => r.Subscriber.Lastname;
					default:
						return null;
				}
			});
			var rents = await _unitOfWork.RentRepository.GetAllAsync(null, orderBy, IncludeProps, pagingParameterModel?.Skip ?? 0, pagingParameterModel?.Take);
			return Mapper.Map<IEnumerable<RentDto>>(rents);
		}

		public async Task<RentDto> GetById(long id)
		{
			var rent = await _unitOfWork.RentRepository.Get(id, IncludeProps);
			return Mapper.Map<RentDto>(rent);
		}

		public Task<RentDto> Delete(long id)
		{
			throw new NotImplementedException();
		}

		public async Task<RentDto> Update(long id, RentDto dto)
		{
			var rent = Mapper.Map<Rent>(dto);
			if (_unitOfWork.RentRepository.Update(rent))
			{
				await _unitOfWork.Save();
			}
			return Mapper.Map<RentDto>(rent);
		}

		public async Task<RentDto> Create(RentDto dto)
		{
			await Check(dto);

			var rent = Mapper.Map<Rent>(dto);
			await InternalCreate(dto, rent);
			return Mapper.Map<RentDto>(rent);
		}

		private async Task InternalCreate(RentDto dto, Rent rent)
		{
			rent.Date = dto.Date.ToLocalTime();
			if (_unitOfWork.RentRepository.Create(rent))
			{
				await _unitOfWork.Save();
			}
		}

		private async Task Check(RentDto dto)
		{
			ThrowIfCountIsZero(dto);
			ThrowIfBookOrSubscriberIsNull(dto);
			ThrowIfRentCountMoreBookCount(dto);
			await ThrowIfCountOfReservedMoreOrEqualCountOfBook(dto);
		}

		private void ThrowIfRentCountMoreBookCount(RentDto dto)
		{
			if (dto.Count > dto.Book.Count) throw new RentCountMoreCountOfBookException(dto.Book.Name);
		}

		private async Task ThrowIfCountOfReservedMoreOrEqualCountOfBook(RentDto dto)
		{
			var rents =
				await
					_unitOfWork.RentRepository.GetAllAsync(new List<Expression<Func<Rent, bool>>>() {x => x.Book.Id == dto.Book.Id}, null,
						$"{nameof(Rent.Book)}");
			var reserved = rents.Sum(x => x.Count);
			if ((reserved + dto.Count) > dto.Book.Count) throw new NotHasAvailableBooksCountException();
		}

		private void ThrowIfBookOrSubscriberIsNull(RentDto dto)
		{
			if (dto.Subscriber == null || dto.Book == null)
			{
				throw new RentNotHasBookOrSubscriberException();
			}
		}

		private static void ThrowIfCountIsZero(RentDto dto)
		{
			if (dto.Count == 0)
			{
				throw new RentNotHasZeroCountException();
			}
		}
	}
}