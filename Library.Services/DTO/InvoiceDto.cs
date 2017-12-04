using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Library.Common;

namespace Library.Services.DTO
{
	public class InvoiceDto: EntityDto, IInvoice<IncomingBookDto>
	{
		public InvoiceDto()
		{
			IncomingBooks = new List<IncomingBookDto>();
		}

		[DataMember]
		public DateTime Date { get; set; }
		public ICollection<IncomingBookDto> IncomingBooks { get; set; }
	}

	public class IncomingBookDto : EntityDto
	{
		public BookDto Book { get; set; }
		public int Count { get; set; }

	}
}