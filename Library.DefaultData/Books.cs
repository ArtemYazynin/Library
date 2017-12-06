using System;
using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Books
	{
		private static readonly Random Rnd = new Random();

		static Books()
		{
			WithoutAuthorsBook = new Book()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Тестовая книга без авторов",
				Isbn = "Тестовый ISBN",
				Edition = new Edition() { Name = "Тестовое издание", Year = 2017 },
				Publisher = Publishers.Self
			};
			MyEvernoteNotes = new Book()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Мои заметки в Evernote",
				Authors = new List<Author>() { Authors.Yazynin },
				Genres = new List<Genre>() { Genres.CSharp, Genres.JavaScript, Genres.DotNet },
				Publisher = Publishers.Self,
				Edition = new Edition() { Name = "1-е издание", Year = 2017 },
				Isbn = "-",
			};
			JsPocketGuide = new Book()
			{
				Id = Rnd.Next(int.MaxValue),
				Count = 13,
				Authors = new List<Author>() { Authors.Flenagan },
				Name = "JavaScript. Карманный справочник",
				Genres = new List<Genre>() { Genres.JavaScript },
				Isbn = "978-1-449-31685-3",
				Publisher = Publishers.Viliams,
				Edition = new Edition()
				{
					Name = "3-е издание.",
					Year = 2015,
				}
			};
			JsForProfessionals = new Book()
			{
				Id = Rnd.Next(int.MaxValue),
				Count = 24,
				Authors = new List<Author>() { Authors.Rezig, Authors.Ferguson },
				Genres = new List<Genre>() { Genres.JavaScript },
				Name = "JavaScript для профессионалов",
				Isbn = "9781430263913",
				Publisher = Publishers.Viliams,
				Edition = new Edition()
				{
					Name = "2-е издание.",
					Year = 2017,
				}
			};
			JsOptimizingPerfomance = new Book()
			{
				Id = Rnd.Next(int.MaxValue),
				Count = 34,
				Authors = new List<Author>() { Authors.Zakas },
				Genres = new List<Genre>() { Genres.JavaScript },
				Name = "JavaScript. Оптимизация производительности",
				Isbn = "978-5-93286-213-1",
				Publisher = Publishers.SymbolPlus,
				Edition = new Edition()
				{
					Name = "1-е издание.",
					Year = 2012,
				}
			};

			Es6AndNotOnly = new Book()
			{
				Id = Rnd.Next(int.MaxValue),
				Count = 18,
				Authors = new List<Author>() { Authors.Simpson },
				Genres = new List<Genre>() { Genres.JavaScript },
				Name = "ES6 и не только",
				Isbn = "9781491904244",
				Publisher = Publishers.Piter,
				Edition = new Edition()
				{
					Name = "1-е издание.",
					Year = 2017,
				}
			};
			ClrVia = new Book()
			{
				Id = Rnd.Next(int.MaxValue),
				Version = new byte[] { 0, 0, 0, 0, 0, 0, 0, 120 },
				Count = 56,
				Authors = new List<Author>() { Authors.Rihter },
				Genres = new List<Genre>() { Genres.DotNet },
				Name = "CLR via C#. Программирование на платформе Microsoft.NET Framework 4.5 на языке C#",
				Isbn = "978-5-496-00433-6",
				Publisher = Publishers.Piter,
				Edition = new Edition()
				{
					Name = "4-е издание.",
					Year = 2017,
				}
			};
			CSharpCompleteGuide = new Book()
			{
				Id = Rnd.Next(int.MaxValue),
				Count = 33,
				Authors = new List<Author>() { Authors.Shildt },
				Genres = new List<Genre>() { Genres.CSharp },
				Name = "C# 4.0. Полное руководство",
				Isbn = "978-5-8459-1684-6",
				Publisher = Publishers.Viliams,
				Edition = new Edition()
				{
					Name = "1-е издание.",
					Year = 2015,
				}
			};
			CSharp6AndNetPlatform = new Book()
			{
				Id = Rnd.Next(int.MaxValue),
				Count = 20,
				Name = "Язык программирования C# 6.0 и платформа .NET 4.6",
				Isbn = "978-5-8459-2099-7, 978-1-4842-1333-9",
				Publisher = Publishers.Viliams,
				Authors = new List<Author>() { Authors.Troelsen, Authors.Jepkins },
				Genres = new List<Genre>() { Genres.CSharp, Genres.DotNet },
				Edition = new Edition()
				{
					Name = "7-е издание",
					Year = 2016
				}
			};
			AsyncProgrammingCSharp5 = new Book()
			{
				Id = Rnd.Next(int.MaxValue),
				Count = 62,
				Name = "Асинхронное программирование в C# 5.0",
				Isbn = "978-5-97060-281-2, 978-1449-33716-2",
				Publisher = Publishers.DmkPress,
				Authors = new List<Author>() { Authors.Devis },
				Genres = new List<Genre>() { Genres.CSharp },
				Edition = new Edition()
				{
					Name = "1-е издание",
					Year = 2015,
				}
			};
		}

		public static Book WithoutAuthorsBook { get; }

		public static Book MyEvernoteNotes { get; }

		public static Book JsPocketGuide { get; }

		public static Book JsForProfessionals { get; }

		public static Book JsOptimizingPerfomance { get; }

		public static Book Es6AndNotOnly { get; }

		public static Book ClrVia { get; }
		
		public static Book CSharpCompleteGuide { get; }
		

		public static Book CSharp6AndNetPlatform { get; }
		
		public static Book AsyncProgrammingCSharp5 { get; }
	}
}