using System;
using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Invoices
	{
		private static readonly Random Rnd = new Random();

		static Invoices()
		{
			First = new Invoice()
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(1990, 11, 06, 14, 52, 33),
				IncomingBooks = new List<IncomingBook>()
				{
					new IncomingBook()
					{
						Id = Rnd.Next(int.MaxValue),
						Book = Books.JsPocketGuide,
						Count = Books.JsPocketGuide.Count
					},
					new IncomingBook()
					{
						Id = Rnd.Next(int.MaxValue),
						Book = Books.JsForProfessionals,
						Count = Books.JsForProfessionals.Count
					},
					new IncomingBook()
					{
						Id = Rnd.Next(int.MaxValue),
						Book = Books.JsOptimizingPerfomance,
						Count = Books.JsOptimizingPerfomance.Count
					}
				}
			};
			Second = new Invoice()
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(1990, 12, 04, 17, 55, 22),
				IncomingBooks = new List<IncomingBook>()
				{
					new IncomingBook()
					{
						Id = Rnd.Next(int.MaxValue),
						Book = Books.Es6AndNotOnly,
						Count = Books.Es6AndNotOnly.Count
					},
					new IncomingBook()
					{
						Id = Rnd.Next(int.MaxValue),
						Book = Books.ClrVia,
						Count = Books.ClrVia.Count
					}
				}
			};
			Third = new Invoice()
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(1990, 12, 05, 09, 00, 13),
				IncomingBooks = new List<IncomingBook>()
				{
					new IncomingBook()
					{
						Id = Rnd.Next(int.MaxValue),
						Book = Books.CSharpCompleteGuide,
						Count = Books.CSharpCompleteGuide.Count
					},
					new IncomingBook()
					{
						Id = Rnd.Next(int.MaxValue),
						Book = Books.CSharp6AndNetPlatform,
						Count = Books.CSharp6AndNetPlatform.Count
					},
					new IncomingBook()
					{
						Id = Rnd.Next(int.MaxValue),
						Book = Books.AsyncProgrammingCSharp5,
						Count = Books.AsyncProgrammingCSharp5.Count
					}
				}
			};
		}

		public static Invoice First { get; }

		public static Invoice Second { get; }

		public static Invoice Third { get; }
	}
}