using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Web.Controllers.api
{
	[RoutePrefix("api/Subscribers")]
	public class SubscribersController:ApiController
	{
		private readonly ISubscribersService _subscribersService;

		public SubscribersController(ISubscribersService subscribersService)
		{
			_subscribersService = subscribersService;
		}

		public async Task<IEnumerable<SubscriberDto>> Get()
		{
			return await _subscribersService.GetAll();
		}

		public async Task<SubscriberDto> Post(SubscriberDto dto)
		{
			return await _subscribersService.Create(dto);
		}

		public async Task<SubscriberDto> Delete(long id)
		{
			return await _subscribersService.Delete(id);
		}

		public async Task<SubscriberDto> Put(long id, SubscriberDto dto)
		{
			return await _subscribersService.Update(id,dto);
		}
	}
}