using System.Collections.Generic;
using System.Runtime.Serialization;
using Library.Common;

namespace Library.Services.DTO
{
	public class GenreDto: EntityDto, IGenre<GenreDto, BookDto>
	{
		public GenreDto()
		{
			Books = new List<BookDto>();
			Children = new List<GenreDto>();
		}

		[DataMember]
		public string Name { get; set; }

		public GenreDto Parent { get; set; }
		public ICollection<BookDto> Books { get; set; }

		[DataMember]
		public ICollection<GenreDto> Children { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

	public class GenreSimpleDto : EntityDto, IGenre<GenreDto, BookDto>
	{
		[DataMember]
		public string Name { get; set; }


		public GenreDto Parent { get; set ; }


		public ICollection<BookDto> Books { get; set; }


		public ICollection<GenreDto> Children { get; set; }
	}
}