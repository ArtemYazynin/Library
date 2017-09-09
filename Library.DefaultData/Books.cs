using System;
using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Books
	{
		private readonly static Random Rnd = new Random();

		public static Book WithoutAuthorsBook = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "Тестовая книга без авторов",
			//Isbn = "Тестовый ISBN",
			Edition = new Edition() { Name = "Тестовое издание", Year = 2017 },
			Publisher = Publishers.Self
		};

		public static Book MyEvernoteNotes = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "Мои заметки в Evernote",
			Authors = new List<Author>() { new Author() {Lastname = "Язынин", Firstname = "Артем", Middlename = "Дмитриевич"} },
			Genres = new List<Genre>() { Genres.CSharp, Genres.JavaScript, Genres.DotNet },
			Publisher = Publishers.Self,
			Edition = new Edition() { Name = "1-е издание", Year = 2017 },
			Isbn = "-",
		};

		public static Book JsPocketGuide = new Book()
		{
			Id= Rnd.Next(int.MaxValue),
			Count = 13,
			CountAvailable = 13,
			Authors = new List<Author>() { new Author() { Lastname = "Флэнаган", Firstname = "Дэвид" } },
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

		public static Book JsForProfessionals = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 24,
			CountAvailable = 24,
			Authors = new List<Author>()
			{
				new Author() {Lastname = "Резиг", Firstname = "Джон"},
				new Author() {Lastname = "Фергюсон", Firstname = "Расс"}
			},
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

		public static Book JsOptimizingPerfomance = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 34,
			CountAvailable = 34,
			Authors = new List<Author>() { new Author() { Lastname = "Закас", Firstname = "Николас" } },
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

		public static Book Es6AndNotOnly = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 18,
			CountAvailable = 18,
			Authors = new List<Author>() { new Author() { Lastname = "Симпсон", Firstname = "Кайл" } },
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

		public static Book ClrVia = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 56,
			CountAvailable = 56,
			Authors = new List<Author>() { new Author() { Lastname = "Рихтер", Firstname = "Джеффри" } },
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

		public static Book CSharpCompleteGuide = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 33,
			CountAvailable = 33,
			Authors = new List<Author>() { new Author() { Lastname = "Шилдт", Firstname = "Герберт" } },
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

		public static Book CSharp6AndNetPlatform = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 20,
			CountAvailable = 20,
			Name = "Язык программирования C# 6.0 и платформа .NET 4.6",
			Isbn = "978-5-8459-2099-7, 978-1-4842-1333-9",
			Publisher = Publishers.Viliams,
			Authors = new List<Author>()
			{
				new Author() {Lastname = "Троелсен", Firstname = "Эндрю"},
				new Author() {Lastname = "Джепикс", Firstname = "Филипп"}
			},
			Genres = new List<Genre>() { Genres.CSharp, Genres.DotNet },
			Edition = new Edition()
			{
				Name = "7-е издание",
				Year = 2016
			}
		};

		public static Book AsyncProgrammingCSharp5 = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 62,
			CountAvailable = 62,
			Name = "Асинхронное программирование в C# 5.0",
			Isbn = "978-5-97060-281-2, 978-1449-33716-2",
			Publisher = Publishers.DmkPress,
			Authors = new List<Author>()
			{
				new Author() {Lastname = "Дэвис", Firstname = "Алекс"}
			},
			Genres = new List<Genre>() { Genres.CSharp },
			Edition = new Edition()
			{
				Name = "1-е издание",
				Year = 2015,
			}
		};
	}
}