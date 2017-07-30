using System.Web.Http;
using Library.Services;

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
		public IHttpActionResult Search(string byName = null, string byAuthors = null, string byMultipleAuthors = null, string byAll = null, bool withoutAuthors = false)
		{
			return Ok();
		}
	}
}