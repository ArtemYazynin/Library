using System.Collections.Generic;
using System.Web.Http;
using Library.Services;
using Library.Services.DTO;

namespace Library.Web.Controllers.api
{
	public class BooksController:ApiController
	{
		private readonly IBooksService _booksService;

		public BooksController(IBooksService booksService)
		{
			_booksService = booksService;
		}

		public BooksController()
		{
		}

		[HttpGet]
		public IEnumerable<BookDto> Get()
		{
			var books = _booksService.Get();
			return books;
		}
	}
}