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


		public ICollection<BookDto> Books { get; set; }

		public override string ToString()
		{
			return $"{Lastname} {Firstname} {Middlename?? string.Empty}";
		}
	}
}
