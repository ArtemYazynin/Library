using System.Data.Entity;
using Library.ObjectModel.Mapping;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	public class LibraryContext:DbContext
	{
		public LibraryContext() : base("DBConnection")
		{
			Database.SetInitializer<LibraryContext>(new LibraryContextInitializer());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new EditionMap());
			modelBuilder.Configurations.Add(new PublisherMap());
			modelBuilder.Configurations.Add(new AuthorMap());
			modelBuilder.Configurations.Add(new BookMap());
		}

		public IDbSet<Book> Books { get; set; }
		public IDbSet<Author> Authors { get; set; }
	}
}