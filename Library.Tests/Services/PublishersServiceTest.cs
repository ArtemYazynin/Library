using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions.Publisher;
using NUnit.Framework;

namespace Library.Tests.Services
{
	sealed class PublishersServiceTest : ServiceTestsBase
	{
		#region GetAll

		[Test]
		public async Task GetAll_ShouldReturnValid()
		{
			var publishers = await PublishersService.GetAll();

			Assert.That(publishers.Count(), Is.EqualTo(Publishers.Count));
		}

		#endregion


		#region Delete

		[Test]
		public void Delete_PublisherHasBooks_ShouldThrowPublisherHasBooksException()
		{
			var publisher = Publishers.First(x => x.Books.Any());
			Assert.Throws<PublisherHasBooksException>(async () =>
			{
				await PublishersService.Delete(publisher.Id);
			});
		}

		[Test]
		public async Task Delete_ShouldDeletePublisher()
		{
			var oldCount = Publishers.Count;
			var publisher = Publishers.First();
			publisher.Books.Clear();

			await PublishersService.Delete(publisher.Id);

			var newCount = Publishers.Count;

			Assert.That(newCount, Is.EqualTo(oldCount - 1));
			Assert.That(Publishers.SingleOrDefault(x => x.Id == publisher.Id), Is.Null);
		}

		#endregion


		#region Update

		[Test]
		public void Update_ExistingName_ShouldThrownPublisherDublicateException()
		{
			var existingPublisher = Publishers.First();
			PublisherDto dto = new PublisherDto()
			{
				Id = existingPublisher.Id,
				Name = existingPublisher.Name
			};

			Assert.Throws<PublisherDublicateException>(async () => await PublishersService.Update(dto.Id, dto));
		}

		[Test]
		public void Update_NameIncorrect_ShouldThrownPublisherIncorrectException()
		{
			var publisher = Publishers.First();
			PublisherDto dto = new PublisherDto();

			Assert.Throws<PublisherIncorrectException>(async () => await PublishersService.Update(publisher.Id, dto));
		}

		[Test]
		public async Task Update_ShouldUpdate()
		{
			var publisher = Publishers.First();
			PublisherDto dto = new PublisherDto()
			{
				Id = publisher.Id,
				Name = "very updated name"
			};
			await PublishersService.Update(publisher.Id, dto);

			Assert.That(Publishers.Single(x => x.Id == dto.Id).Name, Is.EqualTo(dto.Name));
		}

		#endregion


		#region Create

		[Test]
		public void Create_ExistingName_ShouldThrownPublisherDublicateException()
		{
			var oldCount = Publishers.Count;
			var publisher = Publishers.First();
			var dto = new PublisherDto()
			{
				Name = publisher.Name
			};


			Assert.Throws<PublisherDublicateException>(async ()=> await PublishersService.Create(dto));
			Assert.That(Publishers.Count, Is.EqualTo(oldCount));
		}

		[Test]
		public void Create_InvalidName_ShouldThrownPublisherIncorrectException()
		{
			var oldCount = Publishers.Count;
			var dto = new PublisherDto();
			Assert.Throws<PublisherIncorrectException>(async () => await PublishersService.Create(dto));
			Assert.That(Publishers.Count, Is.EqualTo(oldCount));
		}


		[Test]
		public async Task Create_ShouldCreate()
		{
			PublisherDto dto = new PublisherDto()
			{
				Name = "very created name"
			};
			await PublishersService.Create(dto);


			Assert.That(Publishers.SingleOrDefault(x => string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase)), 
						Is.Not.Null);
		}
		#endregion
	}

	sealed class InvoicesServiceTest: ServiceTestsBase
	{
		#region GetAll

		[Test]
		public async Task GetAll_ShouldReturnValid()
		{
			var invoices = await InvoicesService.GetAll();
			Assert.That(invoices.Count(), Is.EqualTo(Invoices.Count));
		}

		#endregion

		#region Delete

		[Test]
		public async Task Delete_ShouldDelete()
		{
			var first = Invoices.First();
			if (first == null) throw new NullReferenceException($"Invoices collection is empty");

			var booksOldCount = first.IncomingBooks.Select(incomingBook => Books.Single(x => x.Id == incomingBook.Book.Id)).ToDictionary(book => book.Id, book => book.Count);

			await InvoicesService.Delete(first.Id);

			foreach (var incomingBook in first.IncomingBooks)
			{
				var book = Books.Single(x => x.Id == incomingBook.Book.Id);
				var expectedCount = booksOldCount[book.Id] - incomingBook.Count;
				Assert.That(book.Count, Is.EqualTo(expectedCount));
			}
		}

		#endregion

		#region Create

		[Test]
		public async Task Create_ShouldCreated()
		{
			var clrViaCsharpOldCount = Books.Single(x => x.Id == DefaultData.Books.ClrVia.Id).Count;
			var jsPocketGuideOldCount = Books.Single(x => x.Id == DefaultData.Books.JsPocketGuide.Id).Count;
			var jsForProfessionalsOldCount = Books.Single(x => x.Id == DefaultData.Books.JsForProfessionals.Id).Count;


			var dto = new InvoiceDto()
			{
				Date = DateTime.Now,
				IncomingBooks = new List<IncomingBookDto>()
				{
					new IncomingBookDto()
					{
						Book = Mapper.Map<BookDto>(DefaultData.Books.ClrVia),
						Count = 50
					},
					new IncomingBookDto()
					{
						Book = Mapper.Map<BookDto>(DefaultData.Books.JsPocketGuide),
						Count = 100
					},
					new IncomingBookDto()
					{
						Book = Mapper.Map<BookDto>(DefaultData.Books.JsForProfessionals),
						Count = 150
					}
				}
			};
			await InvoicesService.Create(dto);
			var createdInvoice = Invoices.SingleOrDefault(x => x.Date == dto.Date);
			Assert.That(createdInvoice, Is.Not.Null);
			Assert.That(createdInvoice.IncomingBooks.Count, Is.EqualTo(dto.IncomingBooks.Count));
			
			Assert.That(Books.Single(x=>x.Id == DefaultData.Books.ClrVia.Id).Count, 
						Is.EqualTo(clrViaCsharpOldCount + dto.IncomingBooks.Single(x=>x.Book.Id == DefaultData.Books.ClrVia.Id).Count));
			Assert.That(Books.Single(x => x.Id == DefaultData.Books.JsPocketGuide.Id).Count,
						Is.EqualTo(jsPocketGuideOldCount + dto.IncomingBooks.Single(x => x.Book.Id == DefaultData.Books.JsPocketGuide.Id).Count));
			Assert.That(Books.Single(x => x.Id == DefaultData.Books.JsForProfessionals.Id).Count,
						Is.EqualTo(jsForProfessionalsOldCount + dto.IncomingBooks.Single(x => x.Book.Id == DefaultData.Books.JsForProfessionals.Id).Count));
		}

		#endregion
	}
}