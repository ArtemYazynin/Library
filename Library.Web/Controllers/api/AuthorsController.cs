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
		private readonly IBooksService _booksService;

		public AuthorsController(IAuthorsService authorsService, IBooksService booksService)
		{
			_authorsService = authorsService;
			_booksService = booksService;
		}

		public async Task<IEnumerable<AuthorDto>> Get()
		{
			var authors = await _authorsService.GetAll();
			return authors;
		}

		[HttpGet]
		public async Task<AuthorDto> Get(long id)
		{
			var author = await _authorsService.Get(id);
			return author;
		}

		public async Task<EntityDto> Delete(long id)
		{
			var deletedBook = await _authorsService.Delete(id);
			return deletedBook;
		}

		public async Task<EntityDto> Put(long id, AuthorDto authorDto)
		{
			var author = await _authorsService.Update(id, authorDto);
			return author;
		}

		public async Task<EntityDto> Post(AuthorDto authorDto)
		{
			var author = await _authorsService.Create(authorDto);
			return author;
		}

		[HttpGet]
		[Route("RelatedBooks/{id}")]
		public async Task<IEnumerable<string>> RelatedBooks(long id)
		{
			var result = await _booksService.BooksByAuthor(id);
			return result;
		}
	}
}