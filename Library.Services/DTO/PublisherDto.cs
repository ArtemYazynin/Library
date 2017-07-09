using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.Services.DTO
{
	public class PublisherDto: EntityDto, IPublisher<BookDto>
	{
		public PublisherDto()
		{
			Books = new List<BookDto>();
		}

		public string Name { get; set; }
		public ICollection<BookDto> Books { get; set; }
	}
}