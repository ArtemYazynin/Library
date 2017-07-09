using System;
using System.Collections.Generic;

namespace Library.ObjectModel.Models.Base
{
	public interface IInvoice<TBook>
	{
		DateTime Date { get; set; }
		ICollection<TBook> Books { get; set; }
	}
}