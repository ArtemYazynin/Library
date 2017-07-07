using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Models
{
	public class Edition: Entity
	{
		public Edition()
		{
			Books = new List<Book>();
		}

		public string Name { get; set; }
		public int Year { get; set; }

		public virtual ICollection<Book> Books { get; set; }
	}
}