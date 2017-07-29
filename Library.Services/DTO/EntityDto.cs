using System;
using System.Runtime.Serialization;

namespace Library.Services.DTO
{
	[DataContract(IsReference = true)]
	public class EntityDto
	{
		[DataMember]
		public long Id { get; set; }

		[DataMember]
		public DateTime Version { get; set; }
	}
}