using System;
using System.Collections.Generic;

namespace Library.Common
{
	public interface IInvoice<TBook>
	{
		DateTime Date { get; set; }
		ICollection<TBook> Books { get; set; }
	}
}