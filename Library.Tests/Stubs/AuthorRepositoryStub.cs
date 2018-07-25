using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services;
using Moq;

namespace Library.Tests.Stubs
{
	class AuthorRepositoryStub : RepositoryStubBase<Author>
	{
		private readonly IList<Author> _authors;

		public AuthorRepositoryStub(IList<Author> authors)
		{
			_authors = authors;
		}

		public override Mock<IGenericRepository<Author>> Get()
		{
			var stubAuthorRepository = new Mock<IGenericRepository<Author>>();
			stubAuthorRepository.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Author, bool>>>>(),
				It.IsAny<Func<IQueryable<Author>, IOrderedQueryable<Author>>>(),
				It.IsAny<string>(), 0, null))
				.ReturnsAsync((IEnumerable<Expression<Func<Author, bool>>> filters,
					Func<IQueryable<Author>, IOrderedQueryable<Author>> order, string includeProperties, int skip, int? take) =>
				{
					IEnumerable<Author> localEntities = _authors;
					return GetAllStub(localEntities, filters, order);
				});

			stubAuthorRepository.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) => _authors.SingleOrDefault(x => x.Id == id));

			stubAuthorRepository.Setup(x => x.Create(It.IsAny<Author>()))
				.Returns((Author x) =>
				{
					_authors.Add(x);
					return true;
				});
			stubAuthorRepository.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var author = _authors.SingleOrDefault(x => x.Id == id);
					if (author == null) return false;

					_authors.Remove(author);
					return true;
				});
			stubAuthorRepository.Setup(x => x.Delete(It.IsAny<Author>()))
				.Returns<Author>(x =>
				{
					_authors.Remove(x);
					return true;
				});
			stubAuthorRepository.Setup(x => x.Update(It.IsAny<Author>())).Returns((Author dbentity) =>
			{
				var author = _authors.Single(x => x.Id == dbentity.Id);
				author.Lastname = dbentity.Lastname;
				author.Firstname = dbentity.Firstname;
				author.Middlename = dbentity.Middlename;

				return true;
			});
			return stubAuthorRepository;
		}
	}
}