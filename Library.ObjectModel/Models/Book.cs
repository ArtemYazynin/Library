using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Book: Entity, IBook<Edition,Publisher,Genre,Author,Rent, IncomingBook, File>
	{
		public Book()
		{
			Authors = new List<Author>();
			Rents = new List<Rent>();
			Genres = new List<Genre>();
			IncomingBooks = new List<IncomingBook>();
		}

		public string Name { get; set; }

		public string Isbn { get; set; }

		public string Description { get; set; }

		public int Count { get; set; }

		public Edition Edition { get; set; }
		public long EditionId { get; set; }

		public Publisher Publisher { get; set; }
		public long PublisherId { get; set; }

		public File Cover { get; set; }
		public long? CoverId { get; set; }

		public ICollection<Genre> Genres { get; set; }
		public ICollection<Author> Authors { get; set; }
		public ICollection<Rent> Rents { get; set; }
		public ICollection<IncomingBook> IncomingBooks { get; set; }

	}
}
