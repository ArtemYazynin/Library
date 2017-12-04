using System;
using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Invoices
	{
		public static Invoice First => new Invoice()
		{
			Date = new DateTime(1990,11,06,14,52,33),
			IncomingBooks = new List<IncomingBook>()
			{
				new IncomingBook()
				{
					Book = Books.JsPocketGuide,
					Count = Books.JsPocketGuide.Count
				},
				new IncomingBook()
				{
					Book = Books.JsForProfessionals,
					Count = Books.JsForProfessionals.Count
				},
				new IncomingBook()
				{
					Book = Books.JsOptimizingPerfomance,
					Count = Books.JsOptimizingPerfomance.Count
				}
			}
		};

		public static Invoice Second =>new Invoice()
		{
			Date = new DateTime(1990, 12, 04, 17, 55, 22),
			IncomingBooks = new List<IncomingBook>()
			{
				new IncomingBook()
				{
					Book = Books.Es6AndNotOnly,
					Count = Books.Es6AndNotOnly.Count
				},
				new IncomingBook()
				{
					Book = Books.ClrVia,
					Count = Books.ClrVia.Count
				}
			}
		};

		public static Invoice Third => new Invoice()
		{
			Date = new DateTime(1990, 12, 05, 09, 00, 13),
			IncomingBooks = new List<IncomingBook>()
			{
				new IncomingBook()
				{
					Book = Books.CSharpCompleteGuide,
					Count = Books.CSharpCompleteGuide.Count
				},
				new IncomingBook()
				{
					Book = Books.CSharp6AndNetPlatform,
					Count = Books.CSharp6AndNetPlatform.Count
				},
				new IncomingBook()
				{
					Book = Books.AsyncProgrammingCSharp5,
					Count = Books.AsyncProgrammingCSharp5.Count
				}
			}
		};
	}
}