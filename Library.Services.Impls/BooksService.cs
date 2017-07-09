using System.Collections.Generic;
using System.Linq;
using Library.Services.DTO;

namespace Library.Services.Impls
{
	public class BooksService : IBooksService
	{
		private readonly UnitOfWork _unitOfWork = new UnitOfWork();
		public IEnumerable<BookDto> Get()
		{
			var books = _unitOfWork.BookRepository.GetAll();
			var result = books.Select(x => new BookDto()
			{
				Name = x.Name,
				Publisher = new PublisherDto() { Id = x.Publisher.Id, Version = x.Publisher.Version, Name = x.Publisher.Name},
				Genres = x.Genres.SelectMany(g=> new List<GenreDto>() { new GenreDto() { Id = g.Id, Version = g.Version, Name = g.Name } }).ToList(),
				Authors = x.Authors.SelectMany(y => new List<AuthorDto>()
				{
					new AuthorDto()
					{
						Lastname = y.Lastname,
						Firstname = y.Firstname,
						Middlename = y.Middlename
					}
				}).ToList()
			});
			return result;
		}
	}
}