using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Models
{
	public class Author: Entity, IPerson
	{
		public Author()
		{
			Books = new List<Book>();
		}

		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string Middlename { get; set; }

		public ICollection<Book> Books { get; set; }

		public override string ToString()
		{
			return $"{Lastname} {Firstname} {Middlename ?? string.Empty}";
		}
	}
}