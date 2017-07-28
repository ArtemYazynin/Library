using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services;
using Library.Services.DTO;
using Library.Services.Impls;
using Moq;
using NUnit.Framework;

namespace Library.Tests.Services
{
	abstract class ServiceTestsBase
	{
		protected IBooksService BooksService;

		[SetUp]
		public void SetUp()
		{
			var books = new Collection<Book>
			{
				DefaultData.Books.JsPocketGuide,
				DefaultData.Books.Es6AndNotOnly,
				DefaultData.Books.ClrVia
			};

			var stubBookRepository = new Mock<IGenericRepository<Book>>();

			stubBookRepository.Setup(
				x =>
					x.GetAll(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<Func<IQueryable<Book>, IOrderedQueryable<Book>>>(),
						It.IsAny<string>()))
				.Returns(books);

			stubBookRepository.Setup(x => x.Get(It.IsAny<long>())).Returns<long>(id => books.SingleOrDefault(x => x.Id == id));

			stubBookRepository.Setup(x => x.Create(It.IsAny<Book>()))
				.Returns((Book x) =>
				{
					books.Add(x);
					return true;
				});
			stubBookRepository.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var book = books.SingleOrDefault(x => x.Id == id);
					if (book == null)
					{
						return false;
					}
					books.Remove(book);
					return true;
				});
			stubBookRepository.Setup(x => x.Delete(It.IsAny<Book>()))
				.Returns<Book>(x =>
				{
					books.Add(x);
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