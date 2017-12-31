using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions.Invoice;
using NUnit.Framework;

namespace Library.Tests.Services
{
	sealed class InvoicesServiceTest: ServiceTestsBase
	{
		private Random _random = new Random();
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
			var booksOldCount = first.IncomingBooks.Select(incomingBook => Books.Single(x => x.Id == incomingBook.Book.Id)).ToDictionary(book => book.Id, book => book.Count);

			foreach (KeyValuePair<long, int> pair in booksOldCount)
			{
				var rents = Rents.Where(x => x.Book.Id == pair.Key).ToList();
				foreach (var rent in rents)
				{
					Rents.Remove(rent);
				}
			}


			await InvoicesService.Delete(first.Id);

			foreach (var incomingBook in first.IncomingBooks)
			{
				var book = Books.Single(x => x.Id == incomingBook.Book.Id);
				var expectedCount = booksOldCount[book.Id] - incomingBook.Count;
				Assert.That(book.Count, Is.EqualTo(expectedCount));
			}
		}

		[Test]
		public void Delete_HasRents_ShouldThrowInvalidCountException()
		{
			var first = Invoices.First();
			var incomingBook = first.IncomingBooks.First();

			Rents.Add(new Rent(incomingBook.Book, Subscribers.First(), Books.Single(x => x.Id == incomingBook.Book.Id).Count)
			{
				Id = _random.Next(int.MaxValue)
			});

			Assert.Throws<InvoiceCountException>(async ()=> await InvoicesService.Delete(first.Id));
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