using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Invoice: Entity, IInvoice<IncomingBook>
	{
		private readonly ICollection<IncomingBook> _incomingBooks;

		public Invoice()
		{
			Date = DateTime.Now;
			_incomingBooks = new List<IncomingBook>();
		}
		public DateTime Date { get; set; }

		public ICollection<IncomingBook> IncomingBooks
		{
			get { return _incomingBooks; }
			set
			{
				if (value == null)
				{
					_incomingBooks.Clear();
				}
				else
				{
					foreach (var incomingBook in value)
					{
						if (incomingBook != null)
						{
							_incomingBooks.Add(incomingBook);
						}
					}
				}
			}
		}
	}
}