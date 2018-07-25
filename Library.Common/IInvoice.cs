using System;
using System.Collections.Generic;

namespace Library.Common
{
	public interface IInvoice<TIncomingBook>
	{
		DateTime Date { get; }
		ICollection<TIncomingBook> IncomingBooks { get; }
	}
}