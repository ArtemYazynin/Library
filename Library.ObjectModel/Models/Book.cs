using System.Collections.Generic;
using Library.ObjectModel.Mapping;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Models
{
	public class Book: Entity
	{
		public Book()
		{
			Authors = new List<Author>();
			Rents = new List<Rent>();
			Invoices = new List<Invoice>();
			Genres = new List<Genre>();
		}

		public string Name { get; set; }

		public string Isbn { get; set; }

		public string Description { get; set; }

		public int Count { get; set; }

		public int CountAvailable { get; set; }

		public virtual Edition Edition { get; set; }
		public long EditionId { get; set; }

		public virtual Publisher Publisher { get; set; }
		public long PublisherId { get; set; }

		//public virtual File Cover { get; set; }
		public long? CoverId { get; set; }

		public virtual ICollection<Genre> Genres { get; set; }

		public virtual ICollection<Author> Authors { get; set; }
		public virtual ICollection<Rent> Rents { get; set; }
		public virtual ICollection<Invoice> Invoices { get; set; }

	}
}
