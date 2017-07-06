using System;
using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Models
{
	public class Invoice: Entity
	{
		public Invoice()
		{
			Date = DateTime.Now;
		}
		public DateTime Date { get; set; }
		public ICollection<Book> Books { get; set; }
	}
}