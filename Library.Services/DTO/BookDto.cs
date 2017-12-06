using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Library.Common;

namespace Library.Services.DTO
{
	public class BookDto: EntityDto, IBook<EditionDto,PublisherDto, GenreSimpleDto, AuthorDto,RentDto,IncomingBookDto,FileDto>
	{
		public BookDto()
		{
			Genres = new List<GenreSimpleDto>();
			Authors = new List<AuthorDto>();
			Rents = new List<RentDto>();
			IncomingBooks = new List<IncomingBookDto>();
		}

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Isbn { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public int Count { get; set; }

		[DataMember]
		public EditionDto Edition { get; set; }

		[DataMember]
		public PublisherDto Publisher { get; set; }

		[DataMember]
		public FileDto Cover { get; set; }

		#region Genres

		[DataMember]
		public ICollection<GenreSimpleDto> Genres { get; set; }

		[DataMember]
		public string GenresStr => string.Join(", ", Genres);

		#endregion


		#region Authors

		[DataMember]
		public ICollection<AuthorDto> Authors { get; set; }

		[DataMember]
		public string AuthorsStr => string.Join(", ", Authors.Select(x=>x.Fio));

		#endregion


		[DataMember]
		public ICollection<RentDto> Rents { get; set; }

		[DataMember]
		public ICollection<IncomingBookDto> IncomingBooks { get; set; }
	}
}