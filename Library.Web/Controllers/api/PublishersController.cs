using System.Collections.Generic;
using System.Web.Http;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Web.Controllers.api
{
	[RoutePrefix("api/Publishers")]
	public class PublishersController: ApiController
	{
		private readonly IPublishersService _publishersService;

		public PublishersController(IPublishersService publishersService)
		{
			_publishersService = publishersService;
		}

		public IEnumerable<PublisherDto> Get()
		{
			var publishers = _publishersService.GetAll();
			return publishers;
		}
	}
}