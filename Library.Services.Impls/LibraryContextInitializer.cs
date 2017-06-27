using System.Collections.Generic;
using System.Data.Entity;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	class LibraryContextInitializer:DropCreateDatabaseAlways<LibraryContext>
	{
		protected override void Seed(LibraryContext context)
		{
			List<Author> authors = new List<Author>()
			{
				new Author(){
					Lastname = "Толстой",
					Firstname = "Лев",
					Middlename = "Николаевич",
				},
				new Author(){
					Lastname = "Рихтер",
					Firstname = "Джефри",
				},
				new Author(){
					Lastname = "Макконнелл",
					Firstname = "Стив",
				}
			};
			authors.ForEach(x=> context.Authors.Add(x));
			context.SaveChanges();
		}
	}
}