using System.Collections.Generic;
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

		public string Name { get; set; }
		public string Isbn { get; set; }
		public string Description { get; set; }
		public int Count { get; set; }
		public int CountAvailable { get; set; }
		public EditionDto Edition { get; set; }
		public PublisherDto Publisher { get; set; }
		public FileDto Cover { get; set; }

		public ICollection<GenreDto> Genres { get; set; }
		public ICollection<AuthorDto> Authors { get; set; }
		public ICollection<RentDto> Rents { get; set; }
		public ICollection<InvoiceDto> Invoices { get; set; }
	}
}