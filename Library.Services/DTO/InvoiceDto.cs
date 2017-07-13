using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Services.DTO
{
	public class InvoiceDto: EntityDto, IInvoice<BookDto>
	{
		public InvoiceDto()
		{
			Books = new List<BookDto>();
		}

		public DateTime Date { get; set; }
		public ICollection<BookDto> Books { get; set; }
	}
}