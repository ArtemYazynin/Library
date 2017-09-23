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
				config.CreateMap<Rent, RentDto>().ReverseMap();
				config.CreateMap<Subscriber, SubscriberDto>().ReverseMap();
				config.CreateMap<Invoice, InvoiceDto>().ReverseMap();
				
				config.CreateMap<Author, AuthorDto>()
					.ForMember(target => target.Books, opt => opt.Ignore())
					.ReverseMap();

				config.CreateMap<File, FileDto>().ReverseMap();

				config.CreateMap<Genre, GenreDto>()
					.ForMember(target => target.Books, opt => opt.Ignore())
					.ForMember(target => target.Children, opt => opt.Ignore())
					.ReverseMap();


				config.CreateMap<Edition, EditionDto>()
					.ForMember(target => target.Books, opt => opt.Ignore())
					.ReverseMap();

				config.CreateMap<Publisher, PublisherDto>()
					.ForMember(target => target.Books, opt=>opt.Ignore())
					.ReverseMap();

				config.CreateMap<Book, BookDto>().MaxDepth(4).ReverseMap();

				//config.CreateMap<BookDto, Book>().MaxDepth(4);

			});
			Mapper.AssertConfigurationIsValid();
		}
	}
}
