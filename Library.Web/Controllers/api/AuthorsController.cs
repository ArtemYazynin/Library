using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Web.Controllers.api
{
	[RoutePrefix("api/Authors")]
	public class AuthorsController: ApiController
	{
		private readonly IAuthorsService _authorsService;

		public AuthorsController(IAuthorsService authorsService)
		{
			_authorsService = authorsService;
		}

		public async Task<IEnumerable<AuthorDto>> Get()
		{
			var authors = await _authorsService.GetAll();
			return authors;
		}
	}
}