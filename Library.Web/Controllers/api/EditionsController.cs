using System.Collections.Generic;
using System.Web.Http;
using Library.Services;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Web.Controllers.api
{
	[RoutePrefix("api/Editions")]
	public class EditionsController: ApiController
	{
		private readonly IEditionsService _editionsService;

		public EditionsController(IEditionsService editionsService)
		{
			_editionsService = editionsService;
		}

		public IEnumerable<EditionDto> Get()
		{
			var editions = _editionsService.GetAll();
			return editions;
		}
	}
}