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
		private IBooksService _booksService;

		[SetUp]
		public void SetUp()
		{
			var books = new Collection<Book>
			{
				JsPocketGuide,
				Es6AndNotOnly,
				ClrVia
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

			_booksService = new BooksService(unitOfWork);
		}

		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			AutoMapperConfig.Initialize();
		}

		[Test]
		public void Update_ShouldUpdated()
		{
			Assert.That(ClrVia.Name, Is.EqualTo(ClrViaCsharpName));
			Assert.That(ClrVia.Isbn, Is.EqualTo(ClrViaCsharpIsbn));
			Assert.That(ClrVia.Publisher.Name, Is.EqualTo(ClrViaCsharpPublisherName));

			const string newName = "Test Driven Development";
			const string newIsbn = "111-111-111-12";
			const string newPublisherName = "Daria Doncova";
			var bookDto = new BookDto()
			{
				Name = newName,
				Isbn = newIsbn,
				Publisher = new PublisherDto()
				{
					Name = newPublisherName
				}
			};
			_booksService.Update(ClrVia.Id, bookDto);

			Assert.That(ClrVia.Name, Is.EqualTo(newName));
			Assert.That(ClrVia.Isbn, Is.EqualTo(newIsbn));
			Assert.That(ClrVia.Publisher.Name, Is.EqualTo(newPublisherName));
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