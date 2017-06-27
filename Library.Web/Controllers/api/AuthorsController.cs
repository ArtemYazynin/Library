using System.Threading.Tasks;
using System.Web.Http;
using Library.Services;

namespace Library.Web.Controllers.api
{
	public class AuthorsController:ApiController
	{
		private readonly IAuthorsService _authorsService;

		public AuthorsController(IAuthorsService authorsService)
		{
			_authorsService = authorsService;
		}

		[HttpGet]
		public async Task<IHttpActionResult> Get()
		{
			var authors = await _authorsService.Get();
			return Ok(authors);
		}
	}
}