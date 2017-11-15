using System.Collections.Generic;
using System.Threading.Tasks;
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

		public async Task<IEnumerable<PublisherDto>> Get()
		{
			var publishers = await _publishersService.GetAll();
			return publishers;
		}

		public async Task<PublisherDto> Delete(long id)
		{
			var deletedPublisher = await _publishersService.Delete(id);
			return deletedPublisher;
		}
	}
}