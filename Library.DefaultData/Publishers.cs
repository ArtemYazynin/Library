using System;
using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Publishers
	{
		private static readonly Random Rnd = new Random();
		public static Publisher Viliams = new Publisher()
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
		public static Publisher Self = new Publisher()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "Язынин Артем Дмитриевич",
			Books = new List<Book>()
			{
				Books.WithoutAuthorsBook,
				Books.MyEvernoteNotes
			}
		};
		public static Publisher Piter = new Publisher()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "Питер",
			Books = new List<Book>()
			{
				Books.Es6AndNotOnly,
				Books.ClrVia
			}
		};
		public static Publisher DmkPress = new Publisher()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "ДМК Пресс",
			Books = new List<Book>() { Books.AsyncProgrammingCSharp5 }
		};
		public static Publisher SymbolPlus = new Publisher()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "Символ-Плюс",
			Books = new List<Book>() { Books.JsOptimizingPerfomance }
		};
	}
}
