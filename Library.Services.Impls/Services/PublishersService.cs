using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Library.Common;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions.Publisher;
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

		public async Task<IEnumerable<PublisherDto>> GetAll(PagingParameterModel pagingParameterModel)
		{
			var orderBy = Helper.GetOrder<Publisher>(pagingParameterModel, x =>
			{
				switch (x.Name)
				{
					case nameof(Publisher):
						return y => y.Name;
					default:
						return null;
				}
			});
			var publishers = await _unitOfWork.PublisherRepository.GetAllAsync(orderBy: orderBy, skip: pagingParameterModel?.Skip ?? 0, take: pagingParameterModel?.Take);
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
			ThrowIfDtoIncorrect(dto.Name);
			await ThrowIfSamePublisherExists(dto.Name);
			var publisherDb = Mapper.Map<Publisher>(dto);
			_unitOfWork.PublisherRepository.Update(publisherDb);
			await _unitOfWork.Save();
			return Mapper.Map<PublisherDto>(publisherDb);
		}

		private async Task ThrowIfSamePublisherExists(string name)
		{
			List<Expression<Func<Publisher, bool>>> filters = new List<Expression<Func<Publisher, bool>>>()
			{
				x=>x.Name.Trim().ToLower() == name.Trim().ToLower()
			};
			var dublicates = await _unitOfWork.PublisherRepository.GetAllAsync(filters);
			if (dublicates.Any())
			{
				throw new PublisherDublicateException();
			}
		}

		private void ThrowIfDtoIncorrect(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new PublisherIncorrectException();
			}
		}

		public async Task<PublisherDto> Create(PublisherDto dto)
		{
			ThrowIfDtoIncorrect(dto.Name);
			await ThrowIfSamePublisherExists(dto.Name);
			var publisher = Mapper.Map<Publisher>(dto);
			_unitOfWork.PublisherRepository.Create(publisher);
			await _unitOfWork.Save();
			return Mapper.Map<PublisherDto>(publisher);
		}

		public async Task<long> Count()
		{
			return await _unitOfWork.PublisherRepository.Count();
		}
	}
}