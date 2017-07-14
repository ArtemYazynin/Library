using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;

namespace Library.Services.Impls
{
	public class BooksService : IBooksService
	{
		private readonly IUnitOfWork _unitOfWork;

		public BooksService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<BookDto> GetAll()
		{
			var books = _unitOfWork.BookRepository.GetAll();
			var booksDto = Mapper.Map<IEnumerable<Book>, Collection<BookDto>>(books);
			return booksDto;
			//var result = books.Select(x => new BookDto()
			//{
			//	Name = x.Name,
			//	Publisher = new PublisherDto() { Id = x.Publisher.Id, Version = x.Publisher.Version, Name = x.Publisher.Name},
			//	Genres = x.Genres.SelectMany(g=> new List<GenreDto>()
			//	{
			//		new GenreDto() { Id = g.Id, Version = g.Version, Name = g.Name }
			//	}).ToList(),
			//	Authors = x.Authors.SelectMany(y => new List<AuthorDto>()
			//	{
			//		new AuthorDto()
			//		{
			//			Lastname = y.Lastname,
			//			Firstname = y.Firstname,
			//			Middlename = y.Middlename
			//		}
			//	}).ToList()
			//});
			//return result;
		}

		public BookDto Get(long id)
		{
			var book = _unitOfWork.BookRepository.Get(id);
			var dto = Mapper.Map<BookDto>(book);
			return dto;
		}

		public EntityDto Create(BookDto bookDto)
		{
			throw new System.NotImplementedException();
		}

		public EntityDto Update(long id, BookDto bookDto)
		{
			throw new System.NotImplementedException();
		}

		public EntityDto Delete(long id)
		{
			throw new System.NotImplementedException();
		}
	}
}