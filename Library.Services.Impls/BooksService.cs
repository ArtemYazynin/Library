using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.VO;

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

		public IEnumerable<BookDto> Search(Filters filters)
		{
			var expressions = BuildExpressions(filters);
			var books = _unitOfWork.BookRepository.GetAll(expressions);
			var booksDto = Mapper.Map<IEnumerable<Book>, Collection<BookDto>>(books);
			return booksDto;
		}

		private List<Expression<Func<Book, bool>>> BuildExpressions(Filters filters)
		{
			List<Expression<Func<Book, bool>>> expressions = new List<Expression<Func<Book, bool>>>();
			if (!string.IsNullOrEmpty(filters.ByName))
			{
				expressions.Add(x => x.Name.ToLower().Contains(filters.ByName.ToLower()));
			}
			if (!string.IsNullOrEmpty(filters.ByAuthor))
			{
				expressions.Add(x => x.Authors.Any(a => a.Lastname.ToLower().Contains(filters.ByAuthor.ToLower())
												  || a.Firstname.ToLower().Contains(filters.ByAuthor.ToLower())
												  /*|| a.Middlename.ToLower().Contains(filters.ByAuthor.ToLower())*/));
			}
			return expressions;
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
			_unitOfWork.Save();
			return new EntityDto()
			{
				Id = book.Id,
				Version = book.Version
			};
		}

		public EntityDto Update(long id, BookDto bookDto)
		{
			var book = _unitOfWork.BookRepository.Get(id);
			book.Name = bookDto.Name;
			book.Isbn = bookDto.Isbn;
			book.Description = bookDto.Description;
			book.Count = bookDto.Count;
			book.CountAvailable = bookDto.CountAvailable;
			if (book.EditionId != bookDto.Edition.Id 
			 || book.Edition.Name != bookDto.Edition.Name)
			{
				book.Edition = new Edition()
				{
					Name = bookDto.Edition.Name,
					Year = bookDto.Edition.Year
				};
			}
			if (book.PublisherId != bookDto.Publisher.Id)
			{
				book.Publisher = new Publisher()
				{
					Name = bookDto.Publisher.Name
				};
			}

			_unitOfWork.Save();
			return new EntityDto()
			{
				Id = id,
				Version = book.Version
			};
		}

		public EntityDto Delete(long id)
		{
			_unitOfWork.BookRepository.Delete(id);
			_unitOfWork.Save();
			return new EntityDto() {Id = id};
		}
	}
}