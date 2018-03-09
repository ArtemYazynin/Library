using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services.DTO;
using Library.Services.VO;
using NUnit.Framework;

namespace Library.Tests.Services
{
	internal sealed class BookServiceTest : ServiceTestsBase
	{
		#region Without authors

		[Test]
		public async Task SearchWithoutAuthors_Enabled_ShouldReturnBookWithoutAuthors()
		{
			var filters = new Filters() {WithoutAuthors = true};
			var books = await BooksService.Search(filters);

			foreach (var book in books)
			{
				Assert.That(book.Authors, Is.Empty);
			}
		}

		#endregion

		[Test]
		public async Task Count_ShouldReturnCountOfBooks()
		{
			var result = await BooksService.Count();
			Assert.That(result, Is.EqualTo(Books.Count));
		}

		#region By Author

		[Test]
		public void SearchByAuthor_ByPartName_ShouldReturnBook()
		{
			var filters = new Filters() {ByAuthor = "джо"};
			SearchByAuthor(filters);
		}

		[Test]
		public void SearchByAuthor_ByLastFirstMiddleNames_ShouldReturnValidBook()
		{
			var author = DefaultData.Books.MyEvernoteNotes.Authors.First();
			var filters = new Filters()
			{
				ByAuthor =
					$"{author.Lastname.Substring(0, 3)} {author.Firstname.Substring(0, 3)} {author.Middlename.Substring(0, 3)}"
			};
			SearchByAuthor(filters);
		}

		private async Task SearchByAuthor(Filters filters)
		{
			var books = await BooksService.Search(filters);

			Assert.That(books, Is.Not.Empty);
			foreach (var book in books)
			{
				var segments = filters.ByAuthor.ToLower().Split(' ').ToList();
				segments.ForEach(y =>
				{
					Assert.That(book.Authors.Any(x => x.Lastname.ToLower().Contains(y)
					                                  || x.Firstname.ToLower().Contains(y)
					                                  || x.Middlename.ToLower().Contains(y)));
				});

			}
		}

		#endregion


		#region By Name

		[Test]
		public async Task SearchByNameDifferentRegisters_ShouldReturnValidOneBook()
		{
			var filters = new Filters() {ByName = "clr"};
			var books = await BooksService.Search(filters);

			Assert.That(books, Is.Not.Empty);
			foreach (var book in books)
			{
				Assert.That(book.Name, Is.StringContaining(filters.ByName).IgnoreCase);
			}
		}

		[Test]
		public async Task SearchByName_ShouldReturnMultipleBooks()
		{
			var filters = new Filters() {ByName = "javasc"};
			var books = await BooksService.Search(filters);

			foreach (var book in books)
			{
				Assert.That(book.Name, Is.StringContaining(filters.ByName).IgnoreCase);
			}
		}

		[Test]
		public async Task SearchByName_InvalidName_ShouldNotFoundAnything()
		{
			var filters = new Filters() {ByName = "!./*.,!"};
			var books = await BooksService.Search(filters);

			Assert.That(books, Is.Empty);
		}

		#endregion


		[Test]
		public async Task Update_ShouldUpdated()
		{
			const string newName = "Test Driven Development";
			const string newIsbn = "111-111-111-12";
			const string newDescription = "Новое тестовое описание";
			const int newCount = 3459;
			const int newCountAvailable = 50;
			var newEditionDto = new EditionDto() {Name = "новое тестовое издание", Year = 2021, Id = Random.Next(int.MaxValue)};
			var newPublisherDto = new PublisherDto()
			{
				Id = Random.Next(int.MaxValue),
				Name = "Daria Doncova"
			};
			var newAuthors = new List<AuthorDto>()
			{
				new AuthorDto()
				{
					Id = DefaultData.Authors.Ferguson.Id,
					Version =  DefaultData.Authors.Ferguson.Version,
					Lastname = DefaultData.Authors.Ferguson.Lastname,
					Firstname = DefaultData.Authors.Ferguson.Firstname,
					Middlename = DefaultData.Authors.Ferguson.Middlename
				},
				new AuthorDto()
				{
					Id = DefaultData.Authors.Devis.Id,
					Version =  DefaultData.Authors.Devis.Version,
					Lastname = DefaultData.Authors.Devis.Lastname,
					Firstname = DefaultData.Authors.Devis.Firstname,
					Middlename = DefaultData.Authors.Devis.Middlename
				}
			};
			var bookDto = new BookDto()
			{
				Version = DefaultData.Books.ClrVia.Version,
				Name = newName,
				Isbn = newIsbn,
				Description = newDescription,
				Count = newCount,
				Publisher = newPublisherDto,
				Edition = newEditionDto,
				Authors = newAuthors
			};
			await BooksService.Update(DefaultData.Books.ClrVia.Id, bookDto);

			Assert.That(DefaultData.Books.ClrVia.Name, Is.EqualTo(newName));
			Assert.That(DefaultData.Books.ClrVia.Isbn, Is.EqualTo(newIsbn));
			Assert.That(DefaultData.Books.ClrVia.Description, Is.EqualTo(newDescription));
			Assert.That(DefaultData.Books.ClrVia.Count, Is.EqualTo(newCount));

			Assert.That(DefaultData.Books.ClrVia.Publisher.Id, Is.EqualTo(newPublisherDto.Id));
			Assert.That(DefaultData.Books.ClrVia.Publisher.Name, Is.EqualTo(newPublisherDto.Name));

			Assert.That(DefaultData.Books.ClrVia.Edition.Id, Is.EqualTo(newEditionDto.Id));
			Assert.That(DefaultData.Books.ClrVia.Edition.Name, Is.EqualTo(newEditionDto.Name));
			Assert.That(DefaultData.Books.ClrVia.Edition.Year, Is.EqualTo(newEditionDto.Year));

			Assert.That(DefaultData.Books.ClrVia.Authors.Count, Is.EqualTo(newAuthors.Count));
			Assert.That(DefaultData.Books.ClrVia.Authors.Any(x=>x.Id == DefaultData.Authors.Rihter.Id), Is.False);
			Assert.That(DefaultData.Books.ClrVia.Authors.Any(x => x.Id == DefaultData.Authors.Ferguson.Id), Is.True);
			Assert.That(DefaultData.Books.ClrVia.Authors.Any(x => x.Id == DefaultData.Authors.Devis.Id), Is.True);
		}

		[Test]
		public async Task Get_ShouldReturnValidCount()
		{
			var books = await BooksService.GetAll();

			Assert.That(books, Is.Not.Null);
			Assert.That(books.Count(), Is.EqualTo(Books.Count));
		}

		[Test]
		public async Task GetById_ShouldReturnById()
		{
			var book = await BooksService.Get(DefaultData.Books.JsPocketGuide.Id);
			Assert.That(book, Is.Not.Null);
			Assert.That(book.Id, Is.EqualTo(DefaultData.Books.JsPocketGuide.Id));
		}

		[Test]
		public async Task Create_ShouldCreated()
		{
			Assert.That((await BooksService.GetAll()).Count(), Is.EqualTo(Books.Count));

			var oldCount = Books.Count;
			var bookDto = new BookDto
			{
				Id = 253,
				Name = "C# 4.0. Полное руководство",
				Isbn = "978-5-8459-1684-6",
			};
			await BooksService.Create(bookDto);

			var booksAfter = await BooksService.GetAll();
			Assert.That(booksAfter.Count(), Is.EqualTo(oldCount + 1));
			Assert.That(await BooksService.Get(bookDto.Id), Is.Not.Null);
		}

		[Test]
		public async Task DeleteByid_ShouldDeleted()
		{
			var booksBefore = await BooksService.GetAll();
			Assert.That(booksBefore.Count(), Is.EqualTo(Books.Count));

			int oldCount = Books.Count;
			await BooksService.Delete(DefaultData.Books.ClrVia.Id);

			var booksAfter = await BooksService.GetAll();
			Assert.That(booksAfter.Count(), Is.EqualTo(oldCount - 1));
			Assert.That(await BooksService.Get(DefaultData.Books.ClrVia.Id), Is.Null);
		}
	}
}