using System.Collections.Generic;
using Library.Common;

namespace Library.Services.DTO
{
	public class SubscriberDto:EntityDto, ISubscriber<RentDto>
	{
		public SubscriberDto()
		{
			Rents=new List<RentDto>();
		}

		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string Middlename { get; set; }

		public ICollection<RentDto> Rents { get; set; }
	}
}