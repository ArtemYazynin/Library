using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Publisher : Entity, IPublisher<Book>
	{
		public Publisher()
		{
			Books = new List<Book>();
		}

		public string Name { get; set; }
		public virtual ICollection<Book> Books { get; set; }
	}
}