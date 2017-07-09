using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.Services.DTO
{
	public class EditionDto: EntityDto, IEdition<BookDto>
	{
		public EditionDto()
		{
			Books = new List<BookDto>();
		}

		public string Name { get; set; }
		public int Year { get; set; }
		public ICollection<BookDto> Books { get; set; }
	}
}