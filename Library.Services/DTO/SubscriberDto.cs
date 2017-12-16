using System.Collections.Generic;
using System.Runtime.Serialization;
using Library.Common;

namespace Library.Services.DTO
{
	public class SubscriberDto:EntityDto, ISubscriber<RentDto>
	{
		public SubscriberDto()
		{
			Rents=new List<RentDto>();
		}

		[DataMember]
		public string Lastname { get; set; }

		[DataMember]
		public string Firstname { get; set; }

		[DataMember]
		public string Middlename { get; set; }

		public ICollection<RentDto> Rents { get; set; }

		[DataMember]
		public string Fio => $"{Lastname} {Firstname} {Middlename ?? string.Empty}";
	}
}