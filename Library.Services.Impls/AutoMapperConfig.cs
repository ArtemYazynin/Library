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
				config.CreateMap<Rent, RentDto>();
				config.CreateMap<Subscriber, SubscriberDto>();
				config.CreateMap<Invoice, InvoiceDto>();
				config.CreateMap<Author, AuthorDto>();
				config.CreateMap<File, FileDto>();
				config.CreateMap<Genre, GenreDto>();
				config.CreateMap<Edition, EditionDto>();
				config.CreateMap<Publisher, PublisherDto>();
				config.CreateMap<Book, BookDto>().MaxDepth(4);
				
			});
			Mapper.AssertConfigurationIsValid();
		}
	}
}
