using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Edition: Entity, IEdition<Book>
	{
		public Edition()
		{
			Books = new List<Book>();
		}

		public string Name { get; set; }
		public int Year { get; set; }

		public ICollection<Book> Books { get; set; }
	}
}