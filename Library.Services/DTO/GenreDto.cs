using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.Services.DTO
{
	public class GenreDto: EntityDto, IGenre<GenreDto, BookDto>
	{
		public GenreDto()
		{
			Books = new List<BookDto>();
			Children = new List<GenreDto>();
		}

		public string Name { get; set; }
		public GenreDto Parent { get; set; }
		public ICollection<BookDto> Books { get; set; }
		public ICollection<GenreDto> Children { get; set; }
	}
}