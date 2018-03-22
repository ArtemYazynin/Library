using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	class LibraryContextInitializer:DropCreateDatabaseAlways<LibraryContext>
	{

		protected override void Seed(LibraryContext context)
		{
			try
			{
				Genres(context);
				Books(context);
				Invoices(context);
				Subscribers(context);
				Rents(context);

				context.SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				foreach (var eve in e.EntityValidationErrors)
				{
					Trace.WriteLine($"Entity of type \"{eve.Entry.State}\" in state \"{eve.Entry.Entity.GetType().Name}\" has the following validation errors:");
					foreach (var ve in eve.ValidationErrors)
					{
						Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
					}
				}
				throw;
			}
		}

		private static void Genres(LibraryContext context)
		{
			var genres = new List<Genre>()
			{
				DefaultData.Genres.CSharp,
				DefaultData.Genres.LanguageAndTools,
				DefaultData.Genres.DotNet,
				DefaultData.Genres.MicrosoftProgramming,
				DefaultData.Genres.JavaScript,
				DefaultData.Genres.WebProgramming,
				DefaultData.Genres.Programming,
				DefaultData.Genres.ComputersAndTecnology
			};
			genres.ForEach(x => context.Genres.Add(x));
		}

		private void Rents(LibraryContext context)
		{
			List<Rent> rents = new List<Rent>()
			{
				new Rent(new DateTime(2018,2,12), DefaultData.Books.JsPocketGuide, DefaultData.Subscribers.Ivanov,1,true),
				new Rent(new DateTime(2018,2,12), DefaultData.Books.JsPocketGuide, DefaultData.Subscribers.Ivanov,1,true),

				new Rent(new DateTime(2018,2,12), DefaultData.Books.JsOptimizingPerfomance,DefaultData.Subscribers.Ivanov,2,true),
				new Rent(new DateTime(2018,2,12), DefaultData.Books.JsOptimizingPerfomance,DefaultData.Subscribers.Ivanov,2,true),

				new Rent(new DateTime(2018,2,12), DefaultData.Books.CSharp6AndNetPlatform,DefaultData.Subscribers.Petrov,3,true),
				new Rent(new DateTime(2018,2,12), DefaultData.Books.CSharp6AndNetPlatform,DefaultData.Subscribers.Petrov,3,true),

				new Rent(new DateTime(2018,2,12), DefaultData.Books.AsyncProgrammingCSharp5, DefaultData.Subscribers.Petrov,4,true),
				new Rent(new DateTime(2018,2,12), DefaultData.Books.AsyncProgrammingCSharp5, DefaultData.Subscribers.Petrov,4,true),

				new Rent(new DateTime(2018,2,12), DefaultData.Books.ClrVia,DefaultData.Subscribers.Sidorov,5),
				new Rent(new DateTime(2018,2,12), DefaultData.Books.ClrVia,DefaultData.Subscribers.Sidorov,5),

				new Rent(new DateTime(2018,2,12), DefaultData.Books.CSharpCompleteGuide,DefaultData.Subscribers.Maslov,6),
				new Rent(new DateTime(2018,2,12), DefaultData.Books.CSharpCompleteGuide,DefaultData.Subscribers.Maslov,6),

				new Rent(new DateTime(2018,2,12), DefaultData.Books.MyEvernoteNotes, DefaultData.Subscribers.Maslov,7),
				new Rent(new DateTime(2018,2,12), DefaultData.Books.MyEvernoteNotes, DefaultData.Subscribers.Maslov,7),

				new Rent(new DateTime(2018,2,12), DefaultData.Books.WithoutAuthorsBook, DefaultData.Subscribers.Maslov,8),
				new Rent(new DateTime(2018,2,12), DefaultData.Books.WithoutAuthorsBook, DefaultData.Subscribers.Maslov,8),

				new Rent(new DateTime(2018,2,12), DefaultData.Books.Es6AndNotOnly, DefaultData.Subscribers.Sidorov,9),
				new Rent(new DateTime(2018,2,12), DefaultData.Books.Es6AndNotOnly, DefaultData.Subscribers.Sidorov,9),

				new Rent(new DateTime(2018,2,12), DefaultData.Books.JsForProfessionals, DefaultData.Subscribers.Sidorov,10),
				new Rent(new DateTime(2018,2,12), DefaultData.Books.JsForProfessionals, DefaultData.Subscribers.Sidorov,10),
			};
			rents.ForEach(x => context.Rents.Add(x));
		}

		private void Subscribers(LibraryContext context)
		{
			List<Subscriber> subscribers = new List<Subscriber>()
			{
				DefaultData.Subscribers.Ivanov,
				DefaultData.Subscribers.Petrov,
				DefaultData.Subscribers.Sidorov,
				DefaultData.Subscribers.Maslov
			};
			subscribers.ForEach(x => context.Subscribers.Add(x));
		}

		private void Invoices(LibraryContext context)
		{
			List<Invoice> invoices = new List<Invoice>()
			{
				DefaultData.Invoices.First,
				DefaultData.Invoices.Second,
				DefaultData.Invoices.Third
			};
			invoices.ForEach(x => context.Invoices.Add(x));
		}

		private void Books(LibraryContext context)
		{
			var books = new List<Book>()
			{
				DefaultData.Books.JsPocketGuide,
				DefaultData.Books.JsForProfessionals,
				DefaultData.Books.JsOptimizingPerfomance,
				DefaultData.Books.Es6AndNotOnly,
				DefaultData.Books.ClrVia,
				DefaultData.Books.CSharpCompleteGuide,
				DefaultData.Books.CSharp6AndNetPlatform,
				DefaultData.Books.AsyncProgrammingCSharp5,
				DefaultData.Books.MyEvernoteNotes,
				DefaultData.Books.WithoutAuthorsBook
			};
			books.ForEach(x => context.Books.Add(x));
		}
	}
}