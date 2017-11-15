using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions;
using Library.Services.Services;

namespace Library.Services.Impls.Services
{
	public class PublishersService : IPublishersService
	{
		private readonly IUnitOfWork _unitOfWork;

		public PublishersService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<PublisherDto>> GetAll()
		{
			var publishers = await _unitOfWork.PublisherRepository.GetAllAsync();
			var result = Mapper.Map<IEnumerable<Publisher>, Collection<PublisherDto>>(publishers);
			return result;
		}

		public async Task<PublisherDto> Delete(long id)
		{
			var includeProperties = $"{nameof(Publisher.Books)}";
			var publisher = await _unitOfWork.PublisherRepository.Get(id, includeProperties);
			ThrowIfHasBooks(publisher);
			_unitOfWork.PublisherRepository.Delete(publisher);
			await _unitOfWork.Save();
			return Mapper.Map<PublisherDto>(publisher);
		}

		private void ThrowIfHasBooks(Publisher publisher)
		{
			if (publisher.Books.Any())
			{
				throw new PublisherHasBooksException(publisher.Books);
			}
		}

		public async Task<PublisherDto> Update(long id, PublisherDto dto)
		{
			throw new System.NotImplementedException();
		}

		public async Task<PublisherDto> Create(PublisherDto dto)
		{
			throw new System.NotImplementedException();
		}
	}
}