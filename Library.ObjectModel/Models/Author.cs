using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Models
{
	public class Author: Entity, IAuthor<Book>
	{
		public Author()
		{
			Books = new List<Book>();
		}

		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string Middlename { get; set; }

		public virtual ICollection<Book> Books { get; set; }

		public override string ToString()
		{
			return $"{Lastname} {Firstname} {Middlename ?? string.Empty}";
		}
	}
}