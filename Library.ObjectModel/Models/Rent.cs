using System;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Models
{
	public class Rent:Entity
	{
		public Rent()
		{
			Date = DateTime.Now;
		}

		public virtual Book Book { get; set; }
		public long BookId { get; set; }

		public virtual Subscriber Subscriber { get; set; }
		public long SubscriberId { get; set; }

		public int Count { get; set; }

		public bool IsActive { get; set; }

		public DateTime Date { get; set; }
	}
}