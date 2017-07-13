using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Subscriber:Entity, ISubscriber<Rent>
	{
		public Subscriber()
		{
			Rents = new List<Rent>();
		}

		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string Middlename { get; set; }

		public virtual ICollection<Rent> Rents { get; set; }
	}
}