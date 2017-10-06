using System;
using System.Runtime.Serialization;

namespace Library.Services.DTO
{
	//Setting IsReference = true allows the serialization of trees of objects that can reference each other.
	[DataContract(IsReference = true)]
	public class EntityDto
	{
		public EntityDto()
		{
			Version = DateTime.Now;
		}

		[DataMember]
		public long Id { get; set; }

		[DataMember]
		public DateTime Version { get; set; }
	}
}