using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Services.Impls.Services
{
	public class EditionsService : IEditionsService
	{
		private readonly IUnitOfWork _unitOfWork;
		public EditionsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<EditionDto> GetAll()
		{
			var editions = _unitOfWork.EditionRepository.GetAll();
			var result = Mapper.Map<IEnumerable<Edition>, Collection<EditionDto>>(editions);
			return result;
		}
	}
}