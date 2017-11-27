using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions.Genre;
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

		public async Task<IEnumerable<GenreSimpleDto>> GetAll()
		{
			var genres = await _unitOfWork.GenreRepository.GetAllAsync();
			var result = Mapper.Map<IEnumerable<Genre>, Collection<GenreSimpleDto>>(genres);
			return result;
		}

		public async Task<IEnumerable<GenreDto>> GetTree()
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
			ThrowIfDtoIncorrect(dto.Name);
			await ThrowIfSameGenreExists(dto.Name);

			var dbEntity = await _unitOfWork.GenreRepository.Get(id);
			dbEntity.Name = dto.Name;
			_unitOfWork.GenreRepository.Update(dbEntity);
			await _unitOfWork.Save();
			return Mapper.Map<GenreDto>(dbEntity);
		}

		public async Task<GenreDto> Create(GenreDto dto)
		{
			ThrowIfDtoIncorrect(dto.Name);
			await ThrowIfSameGenreExists(dto.Name);

			var genre = Mapper.Map<Genre>(dto);
			_unitOfWork.GenreRepository.Create(genre);
			await _unitOfWork.Save();

			return Mapper.Map<GenreDto>(genre);
		}

		private async Task ThrowIfSameGenreExists(string name)
		{
			List<Expression<Func<Genre, bool>>> filters = new List<Expression<Func<Genre, bool>>>()
			{
				x=>x.Name.Trim().ToLower() == name.Trim().ToLower()
			};
			var dublicates = await _unitOfWork.GenreRepository.GetAllAsync(filters);
			if (dublicates.Any())
			{
				throw new GenreDublicateException();
			}
		}

		private void ThrowIfDtoIncorrect(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new GenreIncorrectException();
			}
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