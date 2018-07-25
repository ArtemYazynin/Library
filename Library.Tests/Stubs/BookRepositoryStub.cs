using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services;
using Moq;

namespace Library.Tests.Stubs
{
	class BookRepositoryStub : RepositoryStubBase<Book>
	{
		private readonly IList<Book> _books;

		public BookRepositoryStub(IList<Book> books)
		{
			_books = books;
		}

		public override Mock<IGenericRepository<Book>> Get()
		{
			var stubBookRepository = new Mock<IGenericRepository<Book>>();

			stubBookRepository.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Book, bool>>>>(),
				It.IsAny<Func<IQueryable<Book>, IOrderedQueryable<Book>>>(),
				It.IsAny<string>(), 0, null))
				.ReturnsAsync((IEnumerable<Expression<Func<Book, bool>>> filters,
					Func<IQueryable<Book>, IOrderedQueryable<Book>> order, string includeProperties, int skip, int? take) =>
				{
					IEnumerable<Book> books = _books;
					return GetAllStub(books, filters, order);
				});

			stubBookRepository.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) => _books.SingleOrDefault(x => x.Id == id));

			stubBookRepository.Setup(x => x.Create(It.IsAny<Book>()))
				.Returns((Book x) =>
				{
					_books.Add(x);
					return true;
				});
			stubBookRepository.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var book = _books.SingleOrDefault(x => x.Id == id);
					if (book == null)
					{
						return false;
					}
					_books.Remove(book);
					return true;
				});
			stubBookRepository.Setup(x => x.Delete(It.IsAny<Book>()))
				.Returns<Book>(x =>
				{
					_books.Add(x);
					return true;
				});
			stubBookRepository.Setup(x => x.Update(It.IsAny<Book>())).Returns((Book dbentity) => true);
			stubBookRepository.Setup(x => x.Count()).ReturnsAsync(() => _books.Count);
			return stubBookRepository;
		}
	}
}