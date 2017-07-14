using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services;
using Library.Services.Impls;
using Moq;
using NUnit.Framework;

namespace Library.Tests.Services.Books
{
	[TestFixture]
	sealed class BookServiceTest
	{
		private IBooksService _booksService;
		[Test]
		public void BookService_Get_ShouldReturnValidCount()
		{
			var books = _booksService.GetAll();

			Assert.That(books, Is.Not.Null);
			Assert.That(books.Count(), Is.EqualTo(3));
		}

		[Test]
		public void BookService_GetById_ShouldReturnValidBookById()
		{
			long id = 2125;

			var book = _booksService.Get(id);
			Assert.That(book, Is.Not.Null);
			Assert.That(book.Id, Is.EqualTo(id));
		}



		[SetUp]
		public void SetUp()
		{
			AutoMapperConfig.Initialize();

			var jsPocket = Mock.Of<Book>(b => b.Id == 2125
			                                  && b.Name == "JavaScript. Карманный справочник"
			                                  && b.Isbn == "978-1-449-31685-3"
			                                  && b.Publisher == Mock.Of<Publisher>());
			IGenericRepository<Book> booksRepository = Mock.Of<IGenericRepository<Book>>(d =>
				d.GetAll(It.IsAny<Expression<Func<Book, bool>>>(),It.IsAny<Func<IQueryable<Book>, IOrderedQueryable<Book>>>(), It.IsAny<string>()) == new List<Book>()
				{
					jsPocket,
					Mock.Of<Book>(b=>b.Id == 113
								  && b.Name == "ES6 и не только"
								  && b.Isbn == "9781491904244"
								  && b.Publisher == Mock.Of<Publisher>()),
					Mock.Of<Book>(b=>b.Id == 10
								  && b.Name == "CLR via C#. Программирование на платформе Microsoft.NET Framework 4.5 на языке C#"
								  && b.Isbn == "978-5-496-00433-6"
								  && b.Publisher == Mock.Of<Publisher>())
				}
				&& d.Get(It.IsAny<long>()) == jsPocket);

			var unitOfWork = Mock.Of<IUnitOfWork>(x=>x.BookRepository == booksRepository);
			_booksService = new BooksService(unitOfWork);
		}
	}
}
