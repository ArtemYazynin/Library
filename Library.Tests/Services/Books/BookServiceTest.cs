using System.Linq;
using Library.Services.DTO;
using NUnit.Framework;

namespace Library.Tests.Services.Books
{
	[TestFixture]
	internal sealed class BookServiceTest : ServiceTestsBase
	{
		[Test]
		public void Update_ShouldUpdated()
		{
			const string newName = "Test Driven Development";
			const string newIsbn = "111-111-111-12";
			const string newPublisherName = "Daria Doncova";
			const string newDescription = "Новое тестовое описание";
			const int newCount = 3459;
			const int newCountAvailable = 50;
			EditionDto neweditionDto = new EditionDto() {Name = "новое тестовое издание", Year = 2021};

			var bookDto = new BookDto()
			{
				Name = newName,
				Isbn = newIsbn,
				Description = newDescription,
				Count = newCount,
				CountAvailable = newCountAvailable,
				Publisher = new PublisherDto()
				{
					Name = newPublisherName
				},
				Edition = neweditionDto
			};
			BooksService.Update(DefaultData.Books.ClrVia.Id, bookDto);

			Assert.That(DefaultData.Books.ClrVia.Name, Is.EqualTo(newName));
			Assert.That(DefaultData.Books.ClrVia.Isbn, Is.EqualTo(newIsbn));
			Assert.That(DefaultData.Books.ClrVia.Description, Is.EqualTo(newDescription));
			Assert.That(DefaultData.Books.ClrVia.Count, Is.EqualTo(newCount));
			Assert.That(DefaultData.Books.ClrVia.CountAvailable, Is.EqualTo(newCountAvailable));
			Assert.That(DefaultData.Books.ClrVia.Publisher.Name, Is.EqualTo(newPublisherName));
			Assert.That(DefaultData.Books.ClrVia.Edition.Name, Is.EqualTo(neweditionDto.Name));
			Assert.That(DefaultData.Books.ClrVia.Edition.Year, Is.EqualTo(neweditionDto.Year));
		}

		[Test]
		public void Get_ShouldReturnValidCount()
		{
			var books = BooksService.GetAll();

			Assert.That(books, Is.Not.Null);
			Assert.That(books.Count(), Is.EqualTo(3));
		}

		[Test]
		public void GetById_ShouldReturnById()
		{
			var book = BooksService.Get(DefaultData.Books.JsPocketGuide.Id);
			Assert.That(book, Is.Not.Null);
			Assert.That(book.Id, Is.EqualTo(DefaultData.Books.JsPocketGuide.Id));
		}

		[Test]
		public void Create_ShouldCreated()
		{
			Assert.That(BooksService.GetAll().Count(), Is.EqualTo(3));

			var bookDto = new BookDto
			{
				Id = 253,
				Name = "C# 4.0. Полное руководство",
				Isbn = "978-5-8459-1684-6"
			};
			BooksService.Create(bookDto);

			Assert.That(BooksService.GetAll().Count(), Is.EqualTo(4));
			Assert.That(BooksService.Get(bookDto.Id), Is.Not.Null);
		}

		[Test]
		public void DeleteByid_ShouldDeleted()
		{
			Assert.That(BooksService.GetAll().Count(), Is.EqualTo(3));

			BooksService.Delete(DefaultData.Books.ClrVia.Id);

			Assert.That(BooksService.GetAll().Count(), Is.EqualTo(2));
			Assert.That(BooksService.Get(DefaultData.Books.ClrVia.Id), Is.Null);
		}
	}
}

//IGenericRepository<Book> booksRepository = Mock.Of<IGenericRepository<Book>>(d =>
//	d.GetAll(It.IsAny<Expression<Func<Book, bool>>>(),
//			 It.IsAny<Func<IQueryable<Book>, 
//			 IOrderedQueryable<Book>>>(), It.IsAny<string>()) == books
//	&& d.Get(It.IsAny<long>()) == jsPocketGuide);