using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions.Subscriber;
using Library.Services.Services;

namespace Library.Services.Impls.Services
{
	public class SubscribersService: ISubscribersService
	{
		private readonly IUnitOfWork _unitOfWork;
		public SubscribersService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<SubscriberDto>> GetAll()
		{
			var orderBy = new Func<IQueryable<Subscriber>, IOrderedQueryable<Subscriber>>(x=>x.OrderBy(y=>y.Lastname));
			var subscribers = await _unitOfWork.SubscriberRepository.GetAllAsync(null, orderBy);
			var result = Mapper.Map<IEnumerable<SubscriberDto>>(subscribers);
			return result;
		}

		public async Task<SubscriberDto> GetById(long id)
		{
			var subscriber = await _unitOfWork.SubscriberRepository.Get(id, nameof(Subscriber.Rents));
			return Mapper.Map<SubscriberDto>(subscriber);
		}

		public async Task<SubscriberDto> Delete(long id)
		{
			var subscriber = await _unitOfWork.SubscriberRepository.Get(id, nameof(Subscriber.Rents));
			ThrowIfHasActiveRents(subscriber);

			if (subscriber.Rents.Any() && subscriber.Rents.All(x => !x.IsActive)) {
				subscriber.IsDeleted = true;
				_unitOfWork.SubscriberRepository.Update(subscriber);
			}
			else {
				_unitOfWork.SubscriberRepository.Delete(subscriber);
			}
			await _unitOfWork.Save();
			return Mapper.Map<SubscriberDto>(subscriber);
		}

		private void ThrowIfHasActiveRents(Subscriber subscriber)
		{
			if (subscriber.Rents.Any(x=>x.IsActive))
			{
				throw new SubscriberHasActiveRentsException(subscriber.Fio);
			}
		}

		public async Task<SubscriberDto> Update(long id, SubscriberDto dto)
		{
			if (id == default(long) || dto.Id == default(long))
			{
				throw new SubscriberHasIncorrectIdException(dto.Id);
			}
			ThrowIfSubscriberIncorrect(dto);
			await ThrowIfSameSubscriberExists(dto);
			var subscriber = Mapper.Map<Subscriber>(dto);
			if (_unitOfWork.SubscriberRepository.Update(subscriber))
			{
				await _unitOfWork.Save();
			}
			return Mapper.Map<SubscriberDto>(subscriber);
		}

		public async Task<SubscriberDto> Create(SubscriberDto dto)
		{
			ThrowIfSubscriberIncorrect(dto);
			await ThrowIfSameSubscriberExists(dto);
			var subscriber = Mapper.Map<Subscriber>(dto);
			if (_unitOfWork.SubscriberRepository.Create(subscriber))
			{
				await _unitOfWork.Save();
			}
			await _unitOfWork.Save();
			return Mapper.Map<SubscriberDto>(subscriber);
		}

		private void ThrowIfSubscriberIncorrect(SubscriberDto dto)
		{
			if (string.IsNullOrEmpty(dto.Lastname) || string.IsNullOrEmpty(dto.Firstname))
			{
				throw new SubscriberIncorrectException();
			}
		}

		private async Task ThrowIfSameSubscriberExists(SubscriberDto dto)
		{
			List<Expression<Func<Subscriber, bool>>> filters = new List<Expression<Func<Subscriber, bool>>>()
			{
				x => x.Lastname.ToLower() == dto.Lastname.ToLower(),
				x => x.Firstname.ToLower() == dto.Firstname.ToLower(),
				x => x.Middlename.ToLower() == dto.Middlename.ToLower()
			};
			var dublicates = await _unitOfWork.SubscriberRepository.GetAllAsync(filters);
			if (dublicates.Any())
			{
				throw new SubscriberDublicateException();
			}
		}
	}
}