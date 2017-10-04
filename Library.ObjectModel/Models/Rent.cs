using System;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Rent:Entity, IRent<Book, Subscriber>
	{
		public Rent()
		{
			Date = DateTime.Now;
		}

		public Book Book { get; set; }
		public long BookId { get; set; }

		public Subscriber Subscriber { get; set; }
		public long SubscriberId { get; set; }

		public int Count { get; set; }

		public bool IsActive { get; set; }

		public DateTime Date { get; set; }
	}
}