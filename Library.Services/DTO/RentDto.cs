using System;
using Library.ObjectModel.Models.Base;

namespace Library.Services.DTO
{
	public class RentDto: EntityDto, IRent<BookDto, SubscriberDto>
	{
		public BookDto Book { get; set; }
		public SubscriberDto Subscriber { get; set; }
		public int Count { get; set; }

		public bool IsActive { get; set; }

		public DateTime Date { get; set; }

		public BookDto BookDto { get; set; }
		public SubscriberDto SubscriberDto { get; set; }
	}
}