﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Services;
using Library.Services.VO;

namespace Library.Services.Impls.Services
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
				expressions.Add(x => x.Name.Contains(filters.ByName));
			}
			if (!string.IsNullOrEmpty(filters.ByAuthor))
			{
				var lastFirstMiddlenameSegments = filters.ByAuthor.Split(' ');
				foreach (var segment in lastFirstMiddlenameSegments)
				{
					var authorSegment = segment.Replace(" ", "");
					expressions.Add(x => x.Authors.Any(a => string.IsNullOrEmpty(a.Middlename)
					? a.Lastname.Contains(authorSegment) || a.Firstname.Contains(authorSegment)
					: a.Lastname.Contains(authorSegment) || a.Firstname.Contains(authorSegment) || a.Middlename.Contains(authorSegment)));
				}
				
			}
			if (filters.WithoutAuthors)
			{
				expressions.Add(x=>!x.Authors.Any());
			}
			if (!string.IsNullOrEmpty(filters.ByMultipleAuthors))
			{
				var authors = filters.ByMultipleAuthors.Split(',');
				foreach (var author in authors)
				{

					var lastFirstMiddlenameSegments = author.Split(' ');
					foreach (var authorSegment in lastFirstMiddlenameSegments)
					{
						expressions.Add(x => x.Authors.Any(a => string.IsNullOrEmpty(a.Middlename)
						? a.Lastname.Contains(authorSegment) || a.Firstname.Contains(authorSegment)
						: a.Lastname.Contains(authorSegment) || a.Firstname.Contains(authorSegment) || a.Middlename.Contains(authorSegment)));
					}
				}
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