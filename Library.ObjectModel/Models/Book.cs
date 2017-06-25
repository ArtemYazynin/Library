using System.Collections.Generic;

namespace Library.ObjectModel.Models
{
	public class Book: Entity
	{
		public Book()
		{
			Authors = new List<Author>();
			Publishers = new List<Publisher>();
			Editions = new List<Edition>();
		}

		public string Name { get; set; }
		public ICollection<Author> Authors { get; set; }
		public ICollection<Publisher> Publishers { get; set; }
		public ICollection<Edition> Editions { get; set; }
	}
}
