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
		}

		public BookDto Get(long id)
		{
			var book = _unitOfWork.BookRepository.Get(id);
			var dto = Mapper.Map<BookDto>(book);
			return dto;
		}

		public EntityDto Create(BookDto bookDto)
		{
			var book = Mapper.Map<Book>(bookDto);
			_unitOfWork.BookRepository.Create(book);
			return new EntityDto()
			{
				Id = book.Id,
				Version = book.Version
			};
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