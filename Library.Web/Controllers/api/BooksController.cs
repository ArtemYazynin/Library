using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Common;
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
		public async Task<IEnumerable<BookDto>> Get([FromUri]PagingParameterModel model)
		{
			var books = await _booksService.GetAll(model.Skip, model.Take);
			await this.AddTotalItemsInHeader(_booksService.Count);
			return books;
		}

		[HttpGet]
		public async Task<BookDto> Get(long id)
		{
			var book = await _booksService.Get(id);
			return book;
		}
		
		[HttpGet]
		[Route("Search")]
		public async Task<IEnumerable<BookDto>> Search(string byName = null, string byAuthor = null, string byMultipleAuthors = null, 
														string byAll = null, bool withoutAuthors = false)
		{
			var filters = new Filters()
			{
				ByName = byName,
				ByAuthor = byAuthor,
				ByMultipleAuthors = byMultipleAuthors,
				ByAll = byAll,
				WithoutAuthors = withoutAuthors
			};
			var books = await _booksService.Search(filters);
			return books;
		}

		public async Task<EntityDto> Post(BookDto bookDto)
		{
			var createBook = await _booksService.Create(bookDto);
			return createBook;
		}

		public async Task<EntityDto> Delete(long id)
		{
			var deletedBook = await _booksService.Delete(id);
			return deletedBook;
		}

		public async Task<EntityDto> Put(long id, BookDto bookDto)
		{
			var book = await _booksService.Update(id,bookDto);
			return book;
		}
	}
}