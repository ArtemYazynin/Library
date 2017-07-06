using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	class LibraryContextInitializer:DropCreateDatabaseAlways<LibraryContext>
	{
		#region Publishers

		private static readonly Publisher Viliams = new Publisher() {Name = "Вильямс"};
		private static readonly Publisher Piter = new Publisher() {Name = "Питер"};
		private static readonly Publisher DmkPress = new Publisher() {Name = "ДМК Пресс"};

		#endregion

		#region Subscribers

		private readonly Subscriber _ivanov = new Subscriber()
		{
			Lastname = "Иванов",
			Firstname = "Иван",
			Middlename = "Иванович"
		};

		private readonly Subscriber _petrov = new Subscriber()
		{
			Lastname = "Петров",
			Firstname = "Петр",
			Middlename = "Петрович"
		};

		private readonly Subscriber _sidorov = new Subscriber()
		{
			Lastname = "Сидоров",
			Firstname = "Матвей",
			Middlename = "Матвеевич"
		};

		private readonly Subscriber _maslov = new Subscriber()
		{
			Lastname = "Маслов",
			Firstname = "Андрей",
			Middlename = "Евгениевич"
		};

		#endregion

		#region Books

		private readonly Book _jsPocketGuide = new Book()
		{
			Count = 13,
			Authors = new List<Author>() {new Author() {Lastname = "Флэнаган", Firstname = "Дэвид"}},
			Name = "JavaScript. Карманный справочник",
			Isbn = "978-1-449-31685-3",
			Publishers = new List<Publisher>() {Viliams},
			Edition = new Edition()
			{
				Name = "3-е издание.",
				Year = 2015,
				EditionTypes = new List<EditionType>()
				{
					Printed
				}
			}
		};

		private readonly Book _jsForProfessionals = new Book()
		{
			Count = 24,
			Authors = new List<Author>()
			{
				new Author() {Lastname = "Резиг", Firstname = "Джон"},
				new Author() {Lastname = "Фергюсон", Firstname = "Расс"}
			},
			Name = "JavaScript для профессионалов",
			Isbn = "9781430263913",
			Publishers = new List<Publisher>() {Viliams},
			Edition = new Edition()
			{
				Name = "2-е издание.",
				Year = 2017,
				EditionTypes = new List<EditionType>()
				{
					Printed
				}
			}
		};

		private readonly Book _jsOptimizingPerfomance = new Book()
		{
			Count = 34,
			Authors = new List<Author>() {new Author() {Lastname = "Закас", Firstname = "Николас"}},
			Name = "JavaScript. Оптимизация производительности",
			Isbn = "978-5-93286-213-1",
			Publishers = new List<Publisher>() {new Publisher() {Name = "Символ-Плюс"}},
			Edition = new Edition()
			{
				Name = "1-е издание.",
				Year = 2012,
				EditionTypes = new List<EditionType>()
				{
					Printed
				}
			}
		};

		private readonly Book _es6AndNotOnly = new Book()
		{
			Count = 18,
			Authors = new List<Author>() {new Author() {Lastname = "Симпсон", Firstname = "Кайл"}},
			Name = "ES6 и не только",
			Isbn = " 9781491904244",
			Publishers = new List<Publisher>() {Piter},
			Edition = new Edition()
			{
				Name = "1-е издание.",
				Year = 2017,
				EditionTypes = new List<EditionType>()
				{
					Printed
				}
			}
		};

		private readonly Book _clrVia = new Book()
		{
			Count = 56,
			Authors = new List<Author>() {new Author() {Lastname = "Рихтер", Firstname = "Джеффри"}},
			Name = "CLR via C#. Программирование на платформе Microsoft.NET Framework 4.5 на языке C#",
			Isbn = "978-5-496-00433-6",
			Publishers = new List<Publisher>() {Piter},
			Edition = new Edition()
			{
				Name = "4-е издание.",
				Year = 2017,
				EditionTypes = new List<EditionType>()
				{
					Printed,
					Electronic
				}
			}
		};

		private readonly Book _cSharpCompleteGuide = new Book()
		{
			Count = 33,
			Authors = new List<Author>() {new Author() {Lastname = "Шилдт", Firstname = "Герберт"}},
			Name = "C# 4.0. Полное руководство",
			Isbn = "978-5-8459-1684-6",
			Publishers = new List<Publisher>() {Viliams},
			Edition = new Edition()
			{
				Name = "1-е издание.",
				Year = 2015,
				EditionTypes = new List<EditionType>()
				{
					Printed
				}
			}
		};

		private readonly Book _cSharp6AndNetPlatform = new Book()
		{
			Count = 20,
			Name = "Язык программирования C# 6.0 и платформа .NET 4.6",
			Isbn = "978-5-8459-2099-7, 978-1-4842-1333-9",
			Publishers = new List<Publisher>() {Viliams},
			Authors = new List<Author>()
			{
				new Author() {Lastname = "Троелсен", Firstname = "Эндрю"},
				new Author() {Lastname = "Джепикс", Firstname = "Филипп"}
			},
			Edition = new Edition()
			{
				Name = "7-е издание",
				Year = 2016,
				EditionTypes = new List<EditionType>()
				{
					Printed
				}
			}
		};

		private readonly Book _asyncProgrammingCSharp5 = new Book()
		{
			Count = 62,
			Name = "Асинхронное программирование в C# 5.0",
			Isbn = "978-5-97060-281-2, 978-1449-33716-2",
			Publishers = new List<Publisher>() {DmkPress},
			Authors = new List<Author>()
			{
				new Author() {Lastname = "Дэвис", Firstname = "Алекс"}
			},
			Edition = new Edition()
			{
				Name = "1-е издание",
				Year = 2015,
				EditionTypes = new List<EditionType>()
				{
					Printed
				}
			}
		};

		#endregion

		#region Edition types

		private static readonly EditionType Printed = new EditionType(EditionTypeEnum.Printed);
		private static readonly EditionType Electronic = new EditionType(EditionTypeEnum.Electronic);

		#endregion


		protected override void Seed(LibraryContext context)
		{
			EditionTypes(context);
			Books(context);
			Invoices(context);
			Subscribers(context);

			Rents(context);

			context.SaveChanges();
		}

		private void Rents(LibraryContext context)
		{
			List<Rent> rents = new List<Rent>()
			{
				new Rent()
				{
					Book = _jsPocketGuide,
					Subscriber = _ivanov,
					Count = 1,
					IsActive = true
				},
				new Rent()
				{
					Book = _jsOptimizingPerfomance,
					Subscriber = _ivanov,
					Count = 1,
					IsActive = true
				},
				new Rent()
				{
					Book = _cSharp6AndNetPlatform,
					Subscriber = _petrov,
					Count = 2,
					IsActive = true
				},
				new Rent()
				{
					Book = _asyncProgrammingCSharp5,
					Subscriber = _petrov,
					Count = 1,
					IsActive = true
				},
				new Rent()
				{
					Book = _clrVia,
					Subscriber = _sidorov,
					Count = 1,
					IsActive = true
				},
				new Rent()
				{
					Book = _cSharpCompleteGuide,
					Subscriber = _maslov,
					Count = 1,
					IsActive = true
				},
				new Rent()
				{
					Book = _asyncProgrammingCSharp5,
					Subscriber = _maslov,
					Count = 1,
					IsActive = true
				},
				new Rent()
				{
					Book = _cSharp6AndNetPlatform,
					Subscriber = _maslov,
					Count = 1,
					IsActive = true
				},
			};
			rents.ForEach(x => context.Rents.Add(x));
		}

		private static void EditionTypes(LibraryContext context)
		{
			List<EditionType> editionTypes = new List<EditionType>() {Printed, Electronic};
			editionTypes.ForEach(x => context.EditionTypes.Add(x));
		}

		private void Subscribers(LibraryContext context)
		{
			List<Subscriber> subscribers = new List<Subscriber>()
			{
				_ivanov,
				_petrov,
				_sidorov,
				_maslov
			};
			subscribers.ForEach(x => context.Subscribers.Add(x));
		}

		private void Invoices(LibraryContext context)
		{
			List<Invoice> invoices = new List<Invoice>()
			{
				new Invoice() {Books = new List<Book>() {_jsPocketGuide, _jsForProfessionals, _jsOptimizingPerfomance}},
				new Invoice() {Books = new List<Book>() {_es6AndNotOnly, _clrVia}},
				new Invoice() {Books = new List<Book>() {_cSharpCompleteGuide, _cSharp6AndNetPlatform, _asyncProgrammingCSharp5}}
			};
			invoices.ForEach(x => context.Invoices.Add(x));
		}

		private void Books(LibraryContext context)
		{
			var books = new List<Book>()
			{
				_jsPocketGuide,
				_jsForProfessionals,
				_jsOptimizingPerfomance,
				_es6AndNotOnly,
				_clrVia,
				_cSharpCompleteGuide,
				_cSharp6AndNetPlatform,
				_asyncProgrammingCSharp5
			};
			books.ForEach(x => context.Books.Add(x));
		}
	}
}