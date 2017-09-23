using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Services;
using Library.Services.DTO;
using Library.Services.Services;
using Library.Services.VO;

namespace Library.Web.Controllers.api
{
	[RoutePrefix("api/Books")]
	public class BooksController:ApiController
	{
		private readonly IBooksService _booksService;

		public BooksController(IBooksService booksService)
		{
			_booksService = booksService;
		}

		[HttpGet]
		public IHttpActionResult Get()
		{
			var books = _booksService.GetAll();
			return Ok(books);
		}

		[HttpGet]
		[Route("Search")]
		public IEnumerable<BookDto> Search(string byName = null, string byAuthor = null, string byMultipleAuthors = null, string byAll = null, bool withoutAuthors = false)
		{
			var filters = new Filters()
			{
				ByName = byName,
				ByAuthor = byAuthor,
				ByMultipleAuthors = byMultipleAuthors,
				ByAll = byAll,
				WithoutAuthors = withoutAuthors
			};
			var books = _booksService.Search(filters);
			return books;
		}

		public async Task<EntityDto> Post(BookDto bookDto)
		{
			var createBook = await _booksService.Create(bookDto);
			return createBook;
		}
	}
}