using System.Collections.Generic;

namespace Library.ObjectModel.Models
{
	public class Publisher: Entity
	{
		public Publisher()
		{
			Books = new List<Book>();
		}

		public string Name { get; set; }
		public ICollection<Book> Books { get; set; }
	}
}