using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
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
			var includeProperties = nameof(Subscriber.Rents);
			var subscribers = await _unitOfWork.SubscriberRepository.GetAllAsync(null, orderBy, includeProperties);
			var result = Mapper.Map<IEnumerable<SubscriberDto>>(subscribers);
			return result;
		}

		public async Task<SubscriberDto> GetById(long id)
		{
			var subscriber = await _unitOfWork.SubscriberRepository.Get(id, nameof(Subscriber.Rents));
			return Mapper.Map<SubscriberDto>(subscriber);
		}

		public Task<SubscriberDto> Delete(long id)
		{
			throw new NotImplementedException();
		}

		public Task<SubscriberDto> Update(long id, SubscriberDto dto)
		{
			throw new NotImplementedException();
		}

		public Task<SubscriberDto> Create(SubscriberDto dto)
		{
			throw new NotImplementedException();
		}
	}
}