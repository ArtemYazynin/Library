using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.Services.DTO
{
	public class AuthorDto: EntityDto, IAuthor<BookDto>
	{
		public AuthorDto()
		{
			Books = new List<BookDto>();
		}

		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string Middlename { get; set; }
		public ICollection<BookDto> Books { get; set; }

		public override string ToString()
		{
			return $"{Lastname} {Firstname} {Middlename?? string.Empty}";
		}
	}
}
