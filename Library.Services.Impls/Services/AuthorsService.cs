using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Services.Impls.Services
{
	public class AuthorsService : IAuthorsService
	{
		private readonly IUnitOfWork _unitOfWork;

		public AuthorsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<AuthorDto>> GetAll()
		{
			IEnumerable<Author> authors = await _unitOfWork.AuthorRepository.GetAllAsync();
			var result = Mapper.Map<IEnumerable<Author>, Collection<AuthorDto>>(authors);
			return result;
		}
	}
}