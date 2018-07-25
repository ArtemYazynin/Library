using System.Collections.Generic;
using System.Runtime.Serialization;
using Library.Common;

namespace Library.Services.DTO
{
	public class AuthorDto: EntityDto, IAuthor<BookDto>
	{
		public AuthorDto()
		{
			Books = new List<BookDto>();
		}

		[DataMember]
		public string Lastname { get; set; }

		[DataMember]
		public string Firstname { get; set; }

		[DataMember]
		public string Middlename { get; set; }

		[DataMember]
		public string Fio => $"{Lastname} {Firstname} {Middlename ?? string.Empty}";


		public ICollection<BookDto> Books { get; set; }
	}
}
