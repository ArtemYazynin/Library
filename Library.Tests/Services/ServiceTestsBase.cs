using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services;
using Library.Services.DTO;
using Library.Services.Impls;
using Library.Services.Impls.Services;
using Library.Services.Services;
using Moq;
using NUnit.Framework;

namespace Library.Tests.Services
{
	abstract class ServiceTestsBase
	{
		protected IBooksService BooksService;

		protected Collection<Book> Books;

		[SetUp]
		public void SetUp()
		{
			Books = new Collection<Book>
			{
				DefaultData.Books.JsPocketGuide,
				DefaultData.Books.JsForProfessionals,
				DefaultData.Books.Es6AndNotOnly,
				DefaultData.Books.ClrVia,
				DefaultData.Books.MyEvernoteNotes,
				DefaultData.Books.WithoutAuthorsBook
			};

			var stubBookRepository = new Mock<IGenericRepository<Book>>();

			stubBookRepository.Setup(
				x =>
					x.GetAll(It.IsAny<IEnumerable<Expression<Func<Book, bool>>>>(), It.IsAny<Func<IQueryable<Book>, IOrderedQueryable<Book>>>(),
						It.IsAny<string>()))
				.Returns((IEnumerable<Expression<Func<Book, bool>>> filters,
						  Func<IQueryable<Book>, IOrderedQueryable<Book>> order,
						  string includeProperties) =>
						{
							IEnumerable<Book> books = Books;
							if (filters != null)
							{
								foreach (var expression in filters)
								{
									books = books.Where(expression.Compile());
								}
							}
							return order?.Invoke(books.AsQueryable()) ?? books;
						});

			stubBookRepository.Setup(x => x.Get(It.IsAny<long>())).Returns<long>(id => Books.SingleOrDefault(x => x.Id == id));

			stubBookRepository.Setup(x => x.Create(It.IsAny<Book>()))
				.Returns((Book x) =>
				{
					Books.Add(x);
					return true;
				});
			stubBookRepository.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var book = Books.SingleOrDefault(x => x.Id == id);
					if (book == null)
					{
						return false;
					}
					Books.Remove(book);
					return true;
				});
			stubBookRepository.Setup(x => x.Delete(It.IsAny<Book>()))
				.Returns<Book>(x =>
				{
					Books.Add(x);
					return true;
				});
			stubBookRepository.Setup(x => x.Update(It.IsAny<Book>())).Returns<BookDto>(x => true);
			var unitOfWork = Mock.Of<IUnitOfWork>(x => x.BookRepository == stubBookRepository.Object);

			BooksService = new BooksService(unitOfWork);
		}

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			AutoMapperConfig.Initialize();
		}
	}
}