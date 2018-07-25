using System.Collections.Generic;
using System.Runtime.Serialization;
using Library.Common;

namespace Library.Services.DTO
{
	public class EditionDto: EntityDto, IEdition<BookDto>
	{
		public EditionDto()
		{
			Books = new List<BookDto>();
		}

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public int Year { get; set; }

		[IgnoreDataMember]
		public ICollection<BookDto> Books { get; set; }
	}
}