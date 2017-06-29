using System.Collections.Generic;

namespace Library.ObjectModel.Models
{
	public class Edition: Entity
	{
		public Edition()
		{
			Books = new List<Book>();
			EditionTypes = new List<EditionType>();
		}

		public string Name { get; set; }
		public int Year { get; set; }
		public virtual ICollection<EditionType> EditionTypes { get; set; }

		public virtual ICollection<Book> Books { get; set; }
	}
}