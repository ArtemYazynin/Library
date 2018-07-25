using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;

namespace Library.Services.Impls
{
	public class AutoMapperConfig
	{
		public static void Initialize()
		{
			Mapper.Initialize((config) =>
			{
				config.CreateMap<Rent, RentDto>()
					.ReverseMap();

				config.CreateMap<Subscriber, SubscriberDto>()
					.ReverseMap();

				config.CreateMap<IncomingBook, IncomingBookDto>()
					.ReverseMap();

				config.CreateMap<Invoice, InvoiceDto>()
					.ReverseMap();

				config.CreateMap<Author, AuthorDto>()
					.ReverseMap();

				config.CreateMap<File, FileDto>()
					.ReverseMap();

				config.CreateMap<Genre, GenreDto>()
					.ReverseMap();
				config.CreateMap<Genre, GenreSimpleDto>()
					.ReverseMap();

				config.CreateMap<Edition, EditionDto>()
					.ReverseMap();

				config.CreateMap<Publisher, PublisherDto>()
					.ReverseMap();

				config.CreateMap<Book, BookDto>()
					.MaxDepth(10)
					.ReverseMap()
					.MaxDepth(10);
			});
			Mapper.AssertConfigurationIsValid();
		}
	}
}
