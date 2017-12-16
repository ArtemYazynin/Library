using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Services.Impls.Services
{
	public class SubscribersService: ISubscribersService
	{
		public Task<IEnumerable<SubscriberDto>> GetAll()
		{
			throw new NotImplementedException();
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