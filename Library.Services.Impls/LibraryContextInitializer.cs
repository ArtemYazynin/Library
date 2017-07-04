using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	class LibraryContextInitializer:DropCreateDatabaseAlways<LibraryContext>
	{

		protected override void Seed(LibraryContext context)
		{
			var viliams = new Publisher() {Name = "Вильямс"};
			var piter = new Publisher() {Name = "Питер" };
			var dmkPress = new Publisher() {Name = "ДМК Пресс" };

			List<EditionType> editionTypes = new List<EditionType>()
			{
				new EditionType(EditionTypeEnum.Printed),
				new EditionType(EditionTypeEnum.Electronic)
			};

			List<Book> books = new List<Book>()
			{
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Флэнаган",Firstname = "Дэвид" } },
					Name = "JavaScript. Карманный справочник",
					Isbn = "978-1-449-31685-3",
					Publishers = new List<Publisher>() { viliams },
					Edition = new Edition() {
												Name = "3-е издание.",
												Year = 2015, 
												EditionTypes =  new List<EditionType>()
												{
													editionTypes.Single(x=>x.Type == EditionTypeEnum.Printed)
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
					Isbn = "9781430263913",
					Publishers = new List<Publisher>() { viliams },
					Edition = new Edition() {Name = "2-е издание.",Year = 2017, EditionTypes =  new List<EditionType>()
					{
						editionTypes.Single(x=>x.Type == EditionTypeEnum.Printed)
					}}
				},
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Закас", Firstname = "Николас" } },
					Name = "JavaScript. Оптимизация производительности",
					Isbn = "978-5-93286-213-1",
					Publishers = new List<Publisher>() {new Publisher() { Name = "Символ-Плюс" } },
					Edition = new Edition() {Name = "1-е издание.",Year = 2012, EditionTypes =  new List<EditionType>()
					{
						editionTypes.Single(x=>x.Type == EditionTypeEnum.Printed)
					}}
				},
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Симпсон", Firstname = "Кайл" } },
					Name = "ES6 и не только",
					Isbn = " 9781491904244",
					Publishers = new List<Publisher>() {piter },
					Edition = new Edition() {Name = "1-е издание.",Year = 2017, EditionTypes =  new List<EditionType>()
					{
						editionTypes.Single(x=>x.Type == EditionTypeEnum.Printed)
					} }
				},
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Рихтер", Firstname = "Джеффри" } },
					Name = "CLR via C#. Программирование на платформе Microsoft.NET Framework 4.5 на языке C#",
					Isbn = "978-5-496-00433-6",
					Publishers = new List<Publisher>() {piter},
					Edition = new Edition() {Name = "4-е издание.",Year = 2017, EditionTypes =  new List<EditionType>()
					{
						editionTypes.Single(x=>x.Type == EditionTypeEnum.Printed),
						editionTypes.Single(x=>x.Type == EditionTypeEnum.Electronic)
					}}
				},
				new Book()
				{
					Authors = new List<Author>() {new Author() {Lastname = "Шилдт", Firstname = "Герберт" } },
					Name = "C# 4.0. Полное руководство",
					Isbn = "978-5-8459-1684-6",
					Publishers = new List<Publisher>() {viliams},
					Edition = new Edition()
					{
						Name = "1-е издание.",
						Year = 2015, 
						EditionTypes = new List<EditionType>()
						{
							editionTypes.Single(x=>x.Type == EditionTypeEnum.Printed)
						}
					}
				},
				new Book()
				{
					Name = "Язык программирования C# 6.0 и платформа .NET 4.6",
					Isbn = "978-5-8459-2099-7, 978-1-4842-1333-9",
					Publishers = new List<Publisher>() {viliams },
					Authors = new List<Author>()
					{
						new Author() { Lastname = "Троелсен",Firstname = "Эндрю" },
						new Author() { Lastname = "Джепикс", Firstname = "Филипп"}
					},
					Edition = new Edition() { 
						Name = "7-е издание", 
						Year = 2016,
						EditionTypes = new List<EditionType>()
						{
							editionTypes.Single(x=>x.Type == EditionTypeEnum.Printed)
						}
					}
				},
				new Book()
				{
					Name = "Асинхронное программирование в C# 5.0",
					Isbn = "978-5-97060-281-2, 978-1449-33716-2",
					Publishers = new List<Publisher>() { dmkPress },
					Authors = new List<Author>()
					{
						new Author() { Lastname = "Дэвис", Firstname = "Алекс"}
					},
					Edition = new Edition()
					{
						Name = "1-е издание",
						Year = 2015,
						EditionTypes = new List<EditionType>()
						{
							editionTypes.Single(x=>x.Type == EditionTypeEnum.Printed)
						}
					}
				}
			};

			books.ForEach(x=>context.Books.Add(x));
			context.SaveChanges();
		}
	}
}