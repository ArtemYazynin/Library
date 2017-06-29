using System.Collections.Generic;
using System.Data.Entity;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	class LibraryContextInitializer:DropCreateDatabaseAlways<LibraryContext>
	{
		#region private fields

		private readonly List<Author> _authors = new List<Author>()
		{
			new Author()
			{
				Lastname = "Толстой",
				Firstname = "Лев",
				Middlename = "Николаевич",
			},
			new Author()
			{
				Lastname = "Рихтер",
				Firstname = "Джефри",
			},
			new Author()
			{
				Lastname = "Макконнелл",
				Firstname = "Стив",
			}
		};

		private readonly List<Publisher> _publishers = new List<Publisher>()
		{
			new Publisher() {Name = "Издательский дом \"Питер\""},
			new Publisher() {Name = "Эксмо"},
			new Publisher() {Name = "АСТ"},
			new Publisher() {Name = "Стандартинформ"},
			new Publisher() {Name = "Просвещение"},
			new Publisher() {Name = "Эгмонт Россия ЛТД"},
			new Publisher() {Name = "Рипол классик"},
			new Publisher() {Name = "Экзамен"},
			new Publisher() {Name = "Дрофа"},
			new Publisher() {Name = "ОЛМА Медиа Групп"},
			new Publisher() {Name = "Росмэн"},
			new Publisher() {Name = "Вече"},
			new Publisher() {Name = "Феникс"},
			new Publisher() {Name = "Ленанд"}
		};

		#endregion

		protected override void Seed(LibraryContext context)
		{
			//_authors.ForEach(x=> context.Authors.Add(x));
			//_publishers.ForEach(x=>context.Publishers.Add(x));
			var viliams = new Publisher() {Name = "Вильямс"};
			var piter = new Publisher() {Name = "Питер" };
			List<Book> books = new List<Book>()
			{
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Флэнаган",Firstname = "Дэвид" } },
					Name = "JavaScript. Карманный справочник",
					Publishers = new List<Publisher>() { viliams },
					Edition = new Edition() {
												Name = "3-е издание.",
												Year = 2015, 
												EditionTypes =  new List<EditionType>()
												{
													new EditionType(EditionTypeEnum.Printed)
												}
											}
				},
				new Book()
				{
					Authors = new List<Author>()
					{
						new Author() {Lastname = "Резиг", Firstname = "Джон" },
						new Author() {Lastname = "Фергюсон", Firstname = "Расс"}
					},
					Name = "JavaScript для профессионалов",
					Publishers = new List<Publisher>() { viliams },
					Edition = new Edition() {Name = "2-е издание.",Year = 2017, EditionTypes =  new List<EditionType>()
					{
						new EditionType(EditionTypeEnum.Printed)
					}}
				},
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Закас", Firstname = "Николас" } },
					Name = "JavaScript. Оптимизация производительности",
					Publishers = new List<Publisher>() {new Publisher() { Name = "Символ-Плюс" } },
					Edition = new Edition() {Name = "1-е издание.",Year = 2012, EditionTypes =  new List<EditionType>()
					{
						new EditionType(EditionTypeEnum.Printed)
					}}
				},
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Симпсон", Firstname = "Кайл" } },
					Name = "ES6 и не только",
					Publishers = new List<Publisher>() {piter },
					Edition = new Edition() {Name = "1-е издание.",Year = 2017, EditionTypes =  new List<EditionType>()
					{
						new EditionType(EditionTypeEnum.Printed)
					} }
				},
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Рихтер", Firstname = "Джеффри" } },
					Name = "CLR via C#. Программирование на платформе Microsoft.NET Framework 4.5 на языке C#",
					Publishers = new List<Publisher>() {piter},
					Edition = new Edition() {Name = "4-е издание.",Year = 2017, EditionTypes =  new List<EditionType>()
					{
						new EditionType(EditionTypeEnum.Printed)
					}}
				},
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Рихтер", Firstname = "Джеффри" } },
					Name = "CLR via C#. Программирование на платформе Microsoft.NET Framework 4.5 на языке C#",
					Publishers = new List<Publisher>() {piter},
					Edition = new Edition()
					{
						Name = "4-е издание.",
						Year = 2017, 
						EditionTypes = new List<EditionType>()
						{
							new EditionType(EditionTypeEnum.Printed),
							new EditionType(EditionTypeEnum.Electronic)
						}
					}
				},
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Шилдт", Firstname = "Герберт" } },
					Name = "C# 4.0. Полное руководство",
					Publishers = new List<Publisher>() {viliams},
					Edition = new Edition()
					{
						Name = "1-е издание.",
						Year = 2015, 
						EditionTypes = new List<EditionType>()
						{
							new EditionType(EditionTypeEnum.Printed),
						}
					}
				},
			};
			books.ForEach(x=>context.Books.Add(x));
			context.SaveChanges();
		}
	}
}