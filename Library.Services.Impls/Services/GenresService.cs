using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Services.Impls.Services
{
	public class GenresService:IGenresService
	{
		private readonly IUnitOfWork _unitOfWork;

		public GenresService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<GenreDto>> GetAll()
		{
			var genres = await _unitOfWork.GenreRepository.GetAllAsync();
			var result = Mapper.Map<IEnumerable<Genre>, Collection<GenreDto>>(genres);
			return result;
		}
	}
}