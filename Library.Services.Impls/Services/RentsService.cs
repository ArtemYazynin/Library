using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

		public async Task<IEnumerable<RentDto>> GetAll()
		{
			var orderBy = new Func<IQueryable<Rent>, IOrderedQueryable<Rent>>(x => x.OrderBy(y => y.Date));
			var rents = await _unitOfWork.RentRepository.GetAllAsync(null, orderBy, IncludeProps);
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

		public Task<RentDto> Update(long id, RentDto dto)
		{
			throw new NotImplementedException();
		}

		public async Task<RentDto> Create(RentDto dto)
		{
			ThrowIfCountIsZero(dto);
			ThrowIfBookOrSubscriberIsNull(dto);

			var rent = Mapper.Map<Rent>(dto);
			if (rent.Count > rent.Book.Count) throw new RentCountMoreCountOfBookException(rent.Book.Name);
			if (_unitOfWork.RentRepository.Create(rent))
			{
				await _unitOfWork.Save();
			}
			return Mapper.Map<RentDto>(rent);
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