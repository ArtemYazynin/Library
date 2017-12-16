using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Services.DTO;
using NUnit.Framework;

namespace Library.Tests.Services
{
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
			var createdInvoice = Invoices.Last();
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