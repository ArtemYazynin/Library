using System.Collections.Generic;
using System.Web.Http;
using Library.Services;
using Library.Services.DTO;
using Library.Services.VO;
using Newtonsoft.Json.Linq;

namespace Library.Web.Controllers.api
{
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
		[Route("api/Books/Search")]
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
	}
}