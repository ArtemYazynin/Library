using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

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