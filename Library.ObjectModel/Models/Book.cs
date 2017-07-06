using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Models
{
	public class Book: Entity
	{
		public Book()
		{
			Authors = new List<Author>();
			Publishers = new List<Publisher>();
			Rents = new List<Rent>();
			Invoices = new List<Invoice>();
		}

		public string Name { get; set; }

		public string Isbn { get; set; }

		public int Count { get; set; }

		public virtual Edition Edition { get; set; }
		public long EditionId { get; set; }

		public virtual ICollection<Author> Authors { get; set; }
		public virtual ICollection<Publisher> Publishers { get; set; }
		public virtual ICollection<Rent> Rents { get; set; }
		public virtual ICollection<Invoice> Invoices { get; set; }
	}
}
