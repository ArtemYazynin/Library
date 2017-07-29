using System.Collections.Generic;
using System.Runtime.Serialization;
using Library.Common;

namespace Library.Services.DTO
{
	public class PublisherDto: EntityDto, IPublisher<BookDto>
	{
		public PublisherDto()
		{
			Books = new List<BookDto>();
		}

		[DataMember]
		public string Name { get; set; }

		[IgnoreDataMember]
		public ICollection<BookDto> Books { get; set; }
	}
}