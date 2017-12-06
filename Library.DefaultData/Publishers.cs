using System;
using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Publishers
	{
		private static readonly Random Rnd = new Random();

		static Publishers()
		{
			Viliams = new Publisher()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Вильямс",
				Books = new List<Book>()
				{
					Books.JsPocketGuide,
					Books.JsForProfessionals,
					Books.CSharpCompleteGuide,
					Books.CSharp6AndNetPlatform
				}
			};
			Self = new Publisher()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Язынин Артем Дмитриевич",
				Books = new List<Book>()
				{
					Books.WithoutAuthorsBook,
					Books.MyEvernoteNotes
				}
			};
			Piter = new Publisher()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Питер",
				Books = new List<Book>()
				{
					Books.Es6AndNotOnly,
					Books.ClrVia
				}
			};
			DmkPress = new Publisher()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "ДМК Пресс",
				Books = new List<Book>() { Books.AsyncProgrammingCSharp5 }
			};
			SymbolPlus = new Publisher()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Символ-Плюс",
				Books = new List<Book>() { Books.JsOptimizingPerfomance }
			};
		}

		public static Publisher Viliams { get; }
		public static Publisher Self { get; }
		public static Publisher Piter { get; }
		public static Publisher DmkPress { get; }
		public static Publisher SymbolPlus { get; }
	}
}
