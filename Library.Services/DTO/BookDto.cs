using System.Collections.Generic;

namespace Library.Services.DTO
{
	public class BookDto
	{
		public BookDto()
		{
			Authors = new List<AuthorDto>();
		}

		public string Name { get; set; }

		public IEnumerable<AuthorDto> Authors { get; set; }
	}
}