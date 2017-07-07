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
			modelBuilder.Configurations.Add(new GenreMap());
			modelBuilder.Configurations.Add(new EditionMap());
			modelBuilder.Configurations.Add(new PublisherMap());
			modelBuilder.Configurations.Add(new AuthorMap());
			modelBuilder.Configurations.Add(new BookMap());
			modelBuilder.Configurations.Add(new SubscriberMap());
			modelBuilder.Configurations.Add(new RentMap());
			modelBuilder.Configurations.Add(new InvoiceMap());
		}

		public IDbSet<Genre> Genres { get; set; } 
		public IDbSet<Book> Books { get; set; }
		public IDbSet<Author> Authors { get; set; }
		public IDbSet<Publisher> Publishers { get; set; }
		public IDbSet<Edition> Editions { get; set; }
		public IDbSet<Subscriber> Subscribers { get; set; }
		public IDbSet<Rent> Rents { get; set; }
		public IDbSet<Invoice> Invoices { get; set; }
	}
}