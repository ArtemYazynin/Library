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
		private IUnitOfWork _unitOfWork;
		[Test]
		public void Test()
		{
			var bookService = new BooksService(_unitOfWork);
			var books = bookService.Get();
			Assert.That(books.Count(), Is.EqualTo(3));
		}

		[SetUp]
		public void SetUp()
		{
			IGenericRepository<Book> booksRepository = Mock.Of<IGenericRepository<Book>>(d =>
				d.GetAll(It.IsAny<Expression<Func<Book, bool>>>(),It.IsAny<Func<IQueryable<Book>, IOrderedQueryable<Book>>>(), It.IsAny<string>()) == new List<Book>()
				{
					Mock.Of<Book>(b=>b.Id == 2125
								  && b.Name == "JavaScript. Карманный справочник"
								  && b.Isbn == "978-1-449-31685-3"
								  && b.Publisher == Mock.Of<Publisher>()),
					Mock.Of<Book>(b=>b.Id == 113
								  && b.Name == "ES6 и не только"
								  && b.Isbn == "9781491904244"
								  && b.Publisher == Mock.Of<Publisher>()),
					Mock.Of<Book>(b=>b.Id == 10
								  && b.Name == "CLR via C#. Программирование на платформе Microsoft.NET Framework 4.5 на языке C#"
								  && b.Isbn == "978-5-496-00433-6"
								  && b.Publisher == Mock.Of<Publisher>())
				});
			_unitOfWork = Mock.Of<IUnitOfWork>(x=>x.BookRepository == booksRepository);
			
		}
	}
}
