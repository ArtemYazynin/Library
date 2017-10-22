using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Library.Common;

namespace Library.Services.DTO
{
	public class BookDto: EntityDto, IBook<EditionDto,PublisherDto,GenreDto,AuthorDto,RentDto,InvoiceDto,FileDto>
	{
		public BookDto()
		{
			Genres = new List<GenreDto>();
			Authors = new List<AuthorDto>();
			Rents = new List<RentDto>();
			Invoices = new List<InvoiceDto>();
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
		public int CountAvailable { get; set; }

		[DataMember]
		public EditionDto Edition { get; set; }

		[DataMember]
		public PublisherDto Publisher { get; set; }

		[DataMember]
		public FileDto Cover { get; set; }

		#region Genres

		[DataMember]
		public ICollection<GenreDto> Genres { get; set; }

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
		public ICollection<InvoiceDto> Invoices { get; set; }
	}
}