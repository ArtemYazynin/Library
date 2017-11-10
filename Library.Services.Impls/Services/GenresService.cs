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
	public class GenresService:IGenresService
	{
		private readonly IUnitOfWork _unitOfWork;

		public GenresService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<GenreDto>> GetAll()
		{
			var genres = await _unitOfWork.GenreRepository.GetTree(new List<Expression<Func<Genre, bool>>>()
			{
				x=>x.Parent == null
			});
			var result = Mapper.Map<IEnumerable<Genre>, Collection<GenreDto>>(genres);
			return result;
		}

		public async Task<EntityDto> Delete(long id, bool recursivelly)
		{
			var includeProperties = $"{nameof(Genre.Children)}, {nameof(Genre.Parent)}";
			var genre = await _unitOfWork.GenreRepository.Get(id, includeProperties);
			if (recursivelly)
			{
				DeleteChildrenGenres(genre.Children.ToList());
			}
			_unitOfWork.GenreRepository.Delete(genre);
			await _unitOfWork.Save();
			return new EntityDto()
			{
				Id = id
			};
		}

		public async Task<GenreDto> Update(long id, GenreDto dto)
		{
			ThrowIfDtoIncorrect(dto);
			await ThrowIfSameGenreExists(dto);

			var dbEntity = await _unitOfWork.GenreRepository.Get(id);
			if (!dbEntity.Version.SequenceEqual(dto.Version))
			{
				throw new Exception("Genre was updated early. Please refresh page");
			}
			dbEntity.Name = dto.Name;
			_unitOfWork.GenreRepository.Update(dbEntity);
			await _unitOfWork.Save();
			return Mapper.Map<GenreDto>(dbEntity);
		}

		private async Task ThrowIfSameGenreExists(GenreDto dto)
		{
			List<Expression<Func<Genre, bool>>> filters = new List<Expression<Func<Genre, bool>>>()
			{
				x=>x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()
			};
			var dublicates = await _unitOfWork.GenreRepository.GetAllAsync(filters);
			if (dublicates.Any())
			{
				throw new GenreDublicateException();
			}
		}

		private void ThrowIfDtoIncorrect(GenreDto dto)
		{
			if (string.IsNullOrEmpty(dto.Name))
			{
				throw new GenreIncorrectException();
			}
		}

		public async Task<GenreDto> Create(GenreDto dto)
		{
			throw new NotImplementedException();
		}

		private void DeleteChildrenGenres(IList<Genre> genres)
		{
			for (int i = 0, len = genres.Count(); i < len; i++)
			{
				if (genres[i].Children.Any())
				{
					DeleteChildrenGenres(genres[i].Children.ToList());
				}
				_unitOfWork.GenreRepository.Delete(genres[i]);
			}
		}
	}
}