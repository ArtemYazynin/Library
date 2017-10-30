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

		public async Task<EntityDto> Delete(long id)
		{
			_unitOfWork.AuthorRepository.Delete(id);
			await _unitOfWork.Save();
			return new EntityDto() { Id = id };
		}
		public async Task<AuthorDto> Get(long id)
		{
			var author = await _unitOfWork.AuthorRepository.Get(id);
			var dto = Mapper.Map<AuthorDto>(author);
			return dto;
		}

		public async Task<AuthorDto> Update(long id, AuthorDto authorDto)
		{
			var author = Mapper.Map<AuthorDto,Author>(authorDto);
			if (_unitOfWork.AuthorRepository.Update(author))
			{
				await _unitOfWork.Save();
			}
			return Mapper.Map<AuthorDto>(author);
		}

		public async Task<EntityDto> Create(AuthorDto authorDto)
		{
			var author = Mapper.Map<AuthorDto, Author>(authorDto);
			if (_unitOfWork.AuthorRepository.Create(author))
			{
				await _unitOfWork.Save();
			}
			return new EntityDto()
			{
				Id = author.Id,
				Version = author.Version
			};
		}
	}
}