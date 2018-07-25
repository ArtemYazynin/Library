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
			#region invoice first

			First = new Invoice()
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(1990, 11, 06, 14, 52, 33),
			};
			var incomingBooksFirst = new List<IncomingBook>()
			{
				new IncomingBook(First, Books.JsPocketGuide, Books.JsPocketGuide.Count)
				{
					Id = Rnd.Next(int.MaxValue)
				},
				new IncomingBook(First, Books.JsForProfessionals, Books.JsForProfessionals.Count)
				{
					Id = Rnd.Next(int.MaxValue)
				},
				new IncomingBook(First, Books.JsOptimizingPerfomance, Books.JsOptimizingPerfomance.Count)
				{
					Id = Rnd.Next(int.MaxValue)
				}
			};
			First.IncomingBooks = incomingBooksFirst;

			#endregion



			Second = new Invoice()
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(1990, 12, 04, 17, 55, 22),
				IncomingBooks = new List<IncomingBook>()
				{
					new IncomingBook(Second, Books.Es6AndNotOnly,Books.Es6AndNotOnly.Count)
					{
						Id = Rnd.Next(int.MaxValue)
					},
					new IncomingBook(Second,Books.ClrVia,Books.ClrVia.Count)
					{
						Id = Rnd.Next(int.MaxValue)
					}
				}
			};

			Third = new Invoice()
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(1990, 12, 05, 09, 00, 13),
				IncomingBooks = new List<IncomingBook>()
				{
					new IncomingBook(Third,Books.CSharpCompleteGuide,Books.CSharpCompleteGuide.Count)
					{
						Id = Rnd.Next(int.MaxValue),
					},
					new IncomingBook(Third,Books.CSharp6AndNetPlatform,Books.CSharp6AndNetPlatform.Count)
					{
						Id = Rnd.Next(int.MaxValue)
					},
					new IncomingBook(Third,Books.AsyncProgrammingCSharp5,Books.AsyncProgrammingCSharp5.Count)
					{
						Id = Rnd.Next(int.MaxValue)
					}
				}
			};
			
		}

		public static Invoice First { get; }

		public static Invoice Second { get; }

		public static Invoice Third { get; }
	}
}