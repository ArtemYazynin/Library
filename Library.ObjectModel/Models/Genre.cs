using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Genre: Entity, IGenre<Genre, Book>
	{
		public Genre()
		{
			Books = new List<Book>();
			Children = new List<Genre>();
		}

		public string Name { get; set; }
		public Genre Parent { get; set; }
		public long? ParentId { get; set; }

		public ICollection<Book> Books { get; set; }
		public ICollection<Genre> Children { get; set; }

		public override string ToString()
		{
			return $"{Name}";
		}
	}
}