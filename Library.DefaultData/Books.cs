using System;
using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Books
	{
		private readonly static Random Rnd = new Random();
		public static Book MyEvernoteNotes = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "��� ������� � Evernote",
			Genres = new List<Genre>() { Genres.CSharp, Genres.JavaScript, Genres.DotNet },
			Publisher = Publishers.Self,
			Edition = new Edition() { Name = "1-� �������", Year = 2017 },
			Isbn = "-",
		};

		public static Book JsPocketGuide = new Book()
		{
			Id= Rnd.Next(int.MaxValue),
			Count = 13,
			Authors = new List<Author>() { new Author() { Lastname = "��������", Firstname = "�����" } },
			Name = "JavaScript. ��������� ����������",
			Genres = new List<Genre>() { Genres.JavaScript },
			Isbn = "978-1-449-31685-3",
			Publisher = Publishers.Viliams,
			Edition = new Edition()
			{
				Name = "3-� �������.",
				Year = 2015,
			}
		};

		public static Book JsForProfessionals = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 24,
			Authors = new List<Author>()
			{
				new Author() {Lastname = "�����", Firstname = "����"},
				new Author() {Lastname = "��������", Firstname = "����"}
			},
			Genres = new List<Genre>() { Genres.JavaScript },
			Name = "JavaScript ��� ��������������",
			Isbn = "9781430263913",
			Publisher = Publishers.Viliams,
			Edition = new Edition()
			{
				Name = "2-� �������.",
				Year = 2017,
			}
		};

		public static Book JsOptimizingPerfomance = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 34,
			Authors = new List<Author>() { new Author() { Lastname = "�����", Firstname = "�������" } },
			Genres = new List<Genre>() { Genres.JavaScript },
			Name = "JavaScript. ����������� ������������������",
			Isbn = "978-5-93286-213-1",
			Publisher = Publishers.SymbolPlus,
			Edition = new Edition()
			{
				Name = "1-� �������.",
				Year = 2012,
			}
		};

		public static Book Es6AndNotOnly = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 18,
			Authors = new List<Author>() { new Author() { Lastname = "�������", Firstname = "����" } },
			Genres = new List<Genre>() { Genres.JavaScript },
			Name = "ES6 � �� ������",
			Isbn = "9781491904244",
			Publisher = Publishers.Piter,
			Edition = new Edition()
			{
				Name = "1-� �������.",
				Year = 2017,
			}
		};

		public static Book ClrVia = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 56,
			Authors = new List<Author>() { new Author() { Lastname = "������", Firstname = "�������" } },
			Genres = new List<Genre>() { Genres.DotNet },
			Name = "CLR via C#. ���������������� �� ��������� Microsoft.NET Framework 4.5 �� ����� C#",
			Isbn = "978-5-496-00433-6",
			Publisher = Publishers.Piter,
			Edition = new Edition()
			{
				Name = "4-� �������.",
				Year = 2017,
			}
		};

		public static Book CSharpCompleteGuide = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 33,
			Authors = new List<Author>() { new Author() { Lastname = "�����", Firstname = "�������" } },
			Genres = new List<Genre>() { Genres.CSharp },
			Name = "C# 4.0. ������ �����������",
			Isbn = "978-5-8459-1684-6",
			Publisher = Publishers.Viliams,
			Edition = new Edition()
			{
				Name = "1-� �������.",
				Year = 2015,
			}
		};

		public static Book CSharp6AndNetPlatform = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 20,
			Name = "���� ���������������� C# 6.0 � ��������� .NET 4.6",
			Isbn = "978-5-8459-2099-7, 978-1-4842-1333-9",
			Publisher = Publishers.Viliams,
			Authors = new List<Author>()
			{
				new Author() {Lastname = "��������", Firstname = "�����"},
				new Author() {Lastname = "�������", Firstname = "������"}
			},
			Genres = new List<Genre>() { Genres.CSharp, Genres.DotNet },
			Edition = new Edition()
			{
				Name = "7-� �������",
				Year = 2016
			}
		};

		public static Book AsyncProgrammingCSharp5 = new Book()
		{
			Id = Rnd.Next(int.MaxValue),
			Count = 62,
			Name = "����������� ���������������� � C# 5.0",
			Isbn = "978-5-97060-281-2, 978-1449-33716-2",
			Publisher = Publishers.DmkPress,
			Authors = new List<Author>()
			{
				new Author() {Lastname = "�����", Firstname = "�����"}
			},
			Genres = new List<Genre>() { Genres.CSharp },
			Edition = new Edition()
			{
				Name = "1-� �������",
				Year = 2015,
			}
		};
	}
}