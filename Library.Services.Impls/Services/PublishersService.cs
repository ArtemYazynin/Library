using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
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

		public IEnumerable<PublisherDto> GetAll()
		{
			var publishers = _unitOfWork.PublisherRepository.GetAll();
			var result = Mapper.Map<IEnumerable<Publisher>, Collection<PublisherDto>>(publishers);
			return result;
		}
	}
}