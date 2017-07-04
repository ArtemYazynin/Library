using System;
using System.Collections.Generic;

namespace Library.Services.DTO
{
	public class AuthorDto
	{
		public AuthorDto()
		{
			Books = new List<BookDto>();
		}

		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string Middlename { get; set; }
		public IEnumerable<BookDto> Books { get; set; }

		public override string ToString()
		{
			return $"{Lastname} {Firstname} {Middlename?? string.Empty}";
		}
	}
}
