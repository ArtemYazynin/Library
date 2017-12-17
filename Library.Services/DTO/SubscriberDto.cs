using System.Collections.Generic;
using System.Runtime.Serialization;
using Library.Common;
using Library.ObjectModel.Models;

namespace Library.Services.DTO
{
	public class SubscriberDto:EntityDto, ISubscriber<RentDto>, IDeletable
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

		[DataMember]
		public bool IsDeleted { get; set; }
	}
}