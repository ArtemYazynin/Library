using System;
using System.Collections.Generic;
using Library.Common;

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
		public ICollection<Book> Books { get; set; }
	}
}