using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Invoice: Entity, IInvoice<IncomingBook>
	{
		public Invoice()
		{
			Date = DateTime.Now;
			IncomingBooks = new List<IncomingBook>();
		}
		public DateTime Date { get; set; }
		public ICollection<IncomingBook> IncomingBooks { get; set; }
	}
}