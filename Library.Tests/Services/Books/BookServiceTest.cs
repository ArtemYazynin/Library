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

namespace Library.Tests.Services.Books
{
	[TestFixture]
	internal sealed class BookServiceTest : ServiceTestsBase
	{
		[SetUp]
		public void SetUp()
		{
			var books = new Collection<Book>
			{
				JsPocketGuide,
				Es6AndNotOnly,
				ClrVia
			};

			var stub = new Mock<IGenericRepository<Book>>();
			stub.Setup(
				x =>
					x.GetAll(It.IsAny<Expression<Func<Book, bool>>>(), It.IsAny<Func<IQueryable<Book>, IOrderedQueryable<Book>>>(),
						It.IsAny<string>()))
				.Returns(books);
			stub.Setup(x => x.Get(It.IsAny<long>())).Returns<long>(id => books.SingleOrDefault(x => x.Id == id));
			stub.Setup(x => x.Create(It.IsAny<Book>()))
				.Returns((Book x) =>
				{
					books.Add(x);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<long>()))
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
			stub.Setup(x => x.Delete(It.IsAny<Book>()))
				.Returns<Book>(x =>
				{
					books.Add(x);
					return true;
				});
			var unitOfWork = Mock.Of<IUnitOfWork>(x => x.BookRepository == stub.Object);

			_booksService = new BooksService(unitOfWork);
		}

		private IBooksService _booksService;

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			AutoMapperConfig.Initialize();
		}

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
			const long id = 2125;

			var book = _booksService.Get(id);
			Assert.That(book, Is.Not.Null);
			Assert.That(book.Id, Is.EqualTo(id));
		}

		[Test]
		public void Create_ShouldCreated()
		{
			Assert.That(_booksService.GetAll().Count(), Is.EqualTo(3));

			var bookDto = new BookDto
			{
				Id = 253,
				Name = "C# 4.0. Полное руководство",
				Isbn = "978-5-8459-1684-6"
			};
			_booksService.Create(bookDto);

			Assert.That(_booksService.GetAll().Count(), Is.EqualTo(4));
			Assert.That(_booksService.Get(bookDto.Id), Is.Not.Null);
		}

		[Test]
		public void Delete_ShouldDeleted()
		{
		}

		[Test]
		public void DeleteByid_ShouldDeleted()
		{
			Assert.That(_booksService.GetAll().Count(), Is.EqualTo(3));

			_booksService.Delete(ClrVia.Id);

			Assert.That(_booksService.GetAll().Count(), Is.EqualTo(2));
			Assert.That(_booksService.Get(ClrVia.Id), Is.Null);
		}
	}
}

//IGenericRepository<Book> booksRepository = Mock.Of<IGenericRepository<Book>>(d =>
//	d.GetAll(It.IsAny<Expression<Func<Book, bool>>>(),
//			 It.IsAny<Func<IQueryable<Book>, 
//			 IOrderedQueryable<Book>>>(), It.IsAny<string>()) == books
//	&& d.Get(It.IsAny<long>()) == jsPocketGuide);