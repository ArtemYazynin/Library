using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Common;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Impls.Comparers;
using Library.Services.Impls.Services.Builders;
using Library.Services.Services;
using Library.Services.VO;
using LinqKit;

namespace Library.Services.Impls.Services
{
	public class BooksService : IBooksService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly string _allIncludes = $"{nameof(Publisher)},{nameof(Book.Genres)},{nameof(Book.Authors)},{nameof(Book.Edition)}";
		public BooksService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<BookDto>> GetAll(PagingParameterModel pagingParameterModel)
		{
			var orderBy = Helper.GetOrder<Book>(pagingParameterModel, x =>
			{
				switch (x.Name)
				{
					case nameof(Book.Name):
						return b => b.Name;
					case nameof(Book.Isbn):
						return b => b.Isbn;
					case nameof(Book.Publisher):
						return b => b.Publisher.Name;
					case nameof(Book.Count):
						return b => b.Count.ToString();
					default:
						return null;
				}
			});
			var books = await _unitOfWork.BookRepository.GetAllAsync(null, includeProperties: _allIncludes,orderBy: orderBy, skip: pagingParameterModel?.Skip ?? 0, take: pagingParameterModel?.Take);
			var booksDto = Mapper.Map<IEnumerable<Book>, Collection<BookDto>>(books);
			return booksDto;
		}

		public async Task<IEnumerable<BookDto>> Search(Filters filters)
		{
			var exprBuilder = new Builders.ExpressionBuilder(filters);
			new ExpressionBuilderDirector().Construct(exprBuilder);
			var expressions = exprBuilder.GetResult();


			var books = await _unitOfWork.BookRepository.GetAllAsync(expressions, includeProperties: _allIncludes);
			var booksDto = Mapper.Map<IEnumerable<Book>, Collection<BookDto>>(books);
			return booksDto;
		}

		public async Task<BookDto> Get(long id)
		{
			var book = await GetInternal(id);
			var dto = Mapper.Map<BookDto>(book);
			return dto;
		}

		private async Task<Book> GetInternal(long id)
		{
			var includingProperties = $"{nameof(Edition)},{nameof(Publisher)},{nameof(Book.Authors)},{nameof(Book.Genres)}";
			var book = await _unitOfWork.BookRepository.Get(id, includingProperties);
			return book;
		}

		public async Task<EntityDto> Create(BookDto bookDto)
		{
			var book = Mapper.Map<Book>(bookDto);
			_unitOfWork.BookRepository.Create(book);
			await _unitOfWork.Save();
			return new EntityDto()
			{
				Id = book.Id,
				Version = book.Version
			};
		}

		public async Task<EntityDto> Update(long id, BookDto bookDto)
		{
			var dbEntity = await GetInternal(id);
			var entity = Mapper.Map<BookDto, Book>(bookDto);

			dbEntity.Name = entity.Name;
			dbEntity.Isbn = entity.Isbn;
			dbEntity.Description = entity.Description;
			dbEntity.Count = entity.Count;

			if (dbEntity.EditionId != bookDto.Edition.Id)
			{
				dbEntity.Edition = entity.Edition;
			}
			if (dbEntity.PublisherId != bookDto.Publisher.Id)
			{
				dbEntity.Publisher = entity.Publisher;
			}
			EditAuthors(dbEntity, entity);
			EditGenres(dbEntity, entity);
			if (_unitOfWork.BookRepository.Update(dbEntity))
			{
				await _unitOfWork.Save();
			}

			return Mapper.Map<Book, BookDto>(dbEntity);
		}

		private void EditGenres(Book dbEntity, Book entity)
		{
			var deletedGenres = dbEntity.Genres.Except(entity.Genres, new AuthorComparer<Genre>()).ToList();
			var addedGenres = entity.Genres.Except(dbEntity.Genres, new AuthorComparer<Genre>()).ToList();
			deletedGenres.ForEach(x => { dbEntity.Genres.Remove(x); });
			addedGenres.ForEach(x => { dbEntity.Genres.Add(x); });
		}

		private void EditAuthors(Book dbEntity, Book entity)
		{
			var deletedAuthors = dbEntity.Authors.Except(entity.Authors, new AuthorComparer<Author>()).ToList();
			var addedAuthors = entity.Authors.Except(dbEntity.Authors, new AuthorComparer<Author>()).ToList();
			deletedAuthors.ForEach(x => { dbEntity.Authors.Remove(x); });
			addedAuthors.ForEach(x => { dbEntity.Authors.Add(x); });
		}

		public async Task<EntityDto> Delete(long id)
		{
			_unitOfWork.BookRepository.Delete(id);
			await _unitOfWork.Save();
			return new EntityDto() {Id = id};
		}

		public async Task<IEnumerable<string>> BooksByAuthor(long id)
		{
			var filters = new List<Expression<Func<Book, bool>>> {x => x.Authors.Any(y => y.Id == id)};
			var books = await _unitOfWork.BookRepository.GetAllAsync(filters);
			var booksDto = Mapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(books);
			return booksDto.Select(x => x.Name);
		}

		public async Task<long> Count()
		{
			return await _unitOfWork.BookRepository.Count();
		}
	}
}