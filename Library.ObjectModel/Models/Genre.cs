using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Models
{
	public class Genre: Entity
	{
		public Genre()
		{
			Books = new List<Book>();
			Children = new List<Genre>();
		}

		public string Name { get; set; }
		public virtual Genre Parent { get; set; }
		public long? ParentId { get; set; }

		public virtual ICollection<Book> Books { get; set; }
		public virtual ICollection<Genre> Children { get; set; }
	}
}