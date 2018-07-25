using System;
using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Books
	{
		private static readonly Random Rnd = new Random();

		static Books(){
			WithoutAuthorsBook = new Book("Тестовая книга без авторов", "Тестовый ISBN", 
				new Edition("Тестовое издание", 2017), Publishers.Self,0)
			{
				Id = Rnd.Next(int.MaxValue),
			};
			MyEvernoteNotes = new Book("Мои заметки в Evernote", "-", new Edition("1-е издание",2017) ,
				Publishers.Self,0)
			{
				Id = Rnd.Next(int.MaxValue),
				Authors = new List<Author>() { Authors.Yazynin },
				Genres = new List<Genre>() { Genres.CSharp, Genres.JavaScript, Genres.DotNet },
			};
			JsPocketGuide = new Book("JavaScript. Карманный справочник", "978-1-449-31685-3", 
			new Edition("3-е издание.", 2015), Publishers.Viliams, 13)
			{
				Id = Rnd.Next(int.MaxValue),
				Authors = new List<Author>() { Authors.Flenagan },
				Genres = new List<Genre>() { Genres.JavaScript },
			};
			JsForProfessionals = new Book("JavaScript для профессионалов", "9781430263913", 
			new Edition("2-е издание.", 2017), Publishers.Viliams, 24)
			{
				Id = Rnd.Next(int.MaxValue),
				Authors = new List<Author>() { Authors.Rezig, Authors.Ferguson },
				Genres = new List<Genre>() { Genres.JavaScript },
			};
			JsOptimizingPerfomance = new Book("JavaScript. Оптимизация производительности",
				"978-5-93286-213-1", new Edition("1-е издание.", 2012), Publishers.SymbolPlus, 34)
			{
				Id = Rnd.Next(int.MaxValue),
				Authors = new List<Author>() { Authors.Zakas },
				Genres = new List<Genre>() { Genres.JavaScript },
			};

			Es6AndNotOnly = new Book("ES6 и не только", "9781491904244", 
			new Edition("1-е издание.",2017), Publishers.Piter,18)
			{
				Id = Rnd.Next(int.MaxValue),
				Authors = new List<Author>() { Authors.Simpson },
				Genres = new List<Genre>() { Genres.JavaScript },
			};
			ClrVia = new Book("CLR via C#. Программирование на платформе Microsoft.NET Framework 4.5 на языке C#",
			 "978-5-496-00433-6", new Edition("4-е издание.", 2017), Publishers.Piter,56)
			{
				Id = Rnd.Next(int.MaxValue),
				Version = new byte[] { 0, 0, 0, 0, 0, 0, 0, 120 },
				Authors = new List<Author>() { Authors.Rihter },
				Genres = new List<Genre>() { Genres.DotNet },
			};
			CSharpCompleteGuide = new Book("C# 4.0. Полное руководство", "978-5-8459-1684-6", new Edition("1-е издание.",2015),
			 Publishers.Viliams, 33)
			{
				Id = Rnd.Next(int.MaxValue),
				Authors = new List<Author>() { Authors.Shildt },
				Genres = new List<Genre>() { Genres.CSharp },
			};
			CSharp6AndNetPlatform = new Book("Язык программирования C# 6.0 и платформа .NET 4.6", "978-5-8459-2099-7, 978-1-4842-1333-9",
			new Edition("7-е издание", 2016), Publishers.Viliams,20)
			{
				Id = Rnd.Next(int.MaxValue),
				Count = 20,
				Authors = new List<Author>() { Authors.Troelsen, Authors.Jepkins },
				Genres = new List<Genre>() { Genres.CSharp, Genres.DotNet },
			};
			AsyncProgrammingCSharp5 = new Book("Асинхронное программирование в C# 5.0", "978-5-97060-281-2, 978-1449-33716-2",
			new Edition("1-е издание", 2015), Publishers.DmkPress, 62)
			{
				Id = Rnd.Next(int.MaxValue),
				Authors = new List<Author>() { Authors.Devis },
				Genres = new List<Genre>() { Genres.CSharp },
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