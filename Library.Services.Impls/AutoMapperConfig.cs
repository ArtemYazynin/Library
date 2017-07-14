using System.Collections.Generic;
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
				config.CreateMap<Book, BookDto>().MaxDepth(4).PreserveReferences();
				config.CreateMap<Subscriber, SubscriberDto>().PreserveReferences();
				config.CreateMap<Rent, RentDto>().PreserveReferences();
				
				config.CreateMap<Invoice, InvoiceDto>();
				config.CreateMap<Author, AuthorDto>();
				
				config.CreateMap<File, FileDto>();
				config.CreateMap<Edition, EditionDto>();
				config.CreateMap<Genre, GenreDto>();
				config.CreateMap<Publisher, PublisherDto>().ForMember(dto => dto.Books, opt => opt.MapFrom(p => Mapper.Map<IEnumerable<Book>,IEnumerable<BookDto>>(p.Books)));
				
				
				

			});
			Mapper.AssertConfigurationIsValid();
		}
	}
}
