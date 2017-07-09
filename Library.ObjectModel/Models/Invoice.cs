using System;
using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Models
{
	public class Invoice: Entity, IInvoice<Book>
	{
		public Invoice()
		{
			Date = DateTime.Now;
			Books = new List<Book>();
		}
		public DateTime Date { get; set; }
		public virtual ICollection<Book> Books { get; set; }
	}
}