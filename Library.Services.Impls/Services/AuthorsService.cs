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
using Library.Services.Impls.Exceptions.Author;
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

		public async Task<IEnumerable<AuthorDto>> GetAll(PagingParameterModel pagingParameterModel)
		{
			var orderBy = GetOrder(pagingParameterModel);
			IEnumerable<Author> authors = await _unitOfWork.AuthorRepository.GetAllAsync(orderBy: orderBy, skip: pagingParameterModel?.Skip ?? 0, take: pagingParameterModel?.Take);
			var result = Mapper.Map<IEnumerable<Author>, Collection<AuthorDto>>(authors);
			return result;
		}

		private static Func<IQueryable<Author>, IOrderedQueryable<Author>> GetOrder(PagingParameterModel pagingParameterModel)
		{
			if (pagingParameterModel == null) return null;
			var expr = GetOrderByKeySelector(pagingParameterModel);
			if (expr == null) return null;
			if (pagingParameterModel.OrderBy == OrderBy.Asc) return x => x.OrderBy(expr);
			return x => x.OrderByDescending(expr);
		}

		private static Expression<Func<Author, string>> GetOrderByKeySelector(PagingParameterModel pagingParameterModel)
		{
			switch (pagingParameterModel.Name)
			{
				case nameof(Author.Lastname):
					return x => x.Lastname;
				case nameof(Author.Firstname):
					return x => x.Firstname;
				case nameof(Author.Middlename):
					return x => x.Middlename;
				default:
					return null;
			}
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
			ThrowIfAuthorIncorrect(authorDto);
			await ThrowIfSameAuthorExists(authorDto);
			var author = Mapper.Map<AuthorDto,Author>(authorDto);
			if (_unitOfWork.AuthorRepository.Update(author))
			{
				await _unitOfWork.Save();
			}
			return Mapper.Map<AuthorDto>(author);
		}

		public async Task<EntityDto> Create(AuthorDto authorDto)
		{
			ThrowIfAuthorIncorrect(authorDto);
			await ThrowIfSameAuthorExists(authorDto);
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

		public async Task<long> Count()
		{
			return await _unitOfWork.AuthorRepository.Count();
		}

		private void ThrowIfAuthorIncorrect(AuthorDto author)
		{
			if (author == null) throw new ArgumentNullException(nameof(author));
			if (string.IsNullOrEmpty(author.Lastname) || string.IsNullOrEmpty(author.Firstname))
			{
				throw new AuthorIncorrectException();
			}
		}

		private async Task ThrowIfSameAuthorExists(AuthorDto authorDto)
		{
			var filters = new List<Expression<Func<Author, bool>>>()
			{
				x=> string.IsNullOrEmpty(x.Middlename)
					? x.Lastname.ToLower() == authorDto.Lastname.ToLower() && x.Firstname.ToLower() == authorDto.Firstname.ToLower()
					: x.Lastname.ToLower() == authorDto.Lastname.ToLower() && x.Firstname.ToLower() == authorDto.Firstname.ToLower() && x.Middlename.ToLower() == authorDto.Middlename.ToLower()
			};

			IEnumerable<Author> authors = await _unitOfWork.AuthorRepository.GetAllAsync(filters);
			if (authors.Any()) throw new AuthorDublicateException();
		}
	}
}