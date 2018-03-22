using System;
using System.Runtime.Serialization;
using Library.Common;

namespace Library.Services.DTO
{
	public class RentDto: EntityDto, IRent<BookDto, SubscriberDto>
	{
		[DataMember]
		public BookDto Book { get; set; }

		[DataMember]
		public SubscriberDto Subscriber { get; set; }

		[DataMember]
		public int Count { get; set; }

		[DataMember]
		public bool IsActive { get; set; }

		[DataMember]
		public DateTime Date { get; set; }
	}

	public class MostPopularReportRow
	{
		[DataMember]
		public string BookName { get; set; }

		[DataMember]
		public int Count { get; set; }
	}
}