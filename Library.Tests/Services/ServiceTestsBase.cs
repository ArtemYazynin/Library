using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services;
using Library.Services.Impls;
using Library.Services.Impls.Services;
using Library.Services.Services;
using Moq;
using NUnit.Framework;

namespace Library.Tests.Services
{
	[TestFixture]
	abstract class ServiceTestsBase
	{
		protected IBooksService BooksService;
		protected IAuthorsService AuthorsService;
		protected IGenresService GenresService;

		protected Collection<Book> Books;
		protected Collection<Author> Authors;
		protected Collection<Genre> Genres;

		protected Random Random = new Random();

		[SetUp]
		public void SetUp()
		{
			Books = new Collection<Book>
			{
				DefaultData.Books.JsPocketGuide,
				DefaultData.Books.JsForProfessionals,
				DefaultData.Books.Es6AndNotOnly,
				DefaultData.Books.ClrVia,
				DefaultData.Books.MyEvernoteNotes,
				DefaultData.Books.WithoutAuthorsBook
			};
			Authors = new Collection<Author>()
			{
				DefaultData.Authors.Devis,
				DefaultData.Authors.Ferguson,
				DefaultData.Authors.Flenagan,
				DefaultData.Authors.Jepkins,
				DefaultData.Authors.Rezig,
				DefaultData.Authors.Rihter,
				DefaultData.Authors.Shildt,
				DefaultData.Authors.Simpson,
				DefaultData.Authors.Troelsen,
				DefaultData.Authors.Yazynin,
				DefaultData.Authors.Zakas
			};

			Genres = new Collection<Genre>()
			{
				DefaultData.Genres.CSharp,
				DefaultData.Genres.ComputersAndTecnology,
				DefaultData.Genres.DotNet,
				DefaultData.Genres.JavaScript,
				DefaultData.Genres.LanguageAndTools,
				DefaultData.Genres.MicrosoftProgramming,
				DefaultData.Genres.Programming,
				DefaultData.Genres.WebProgramming
			};
			var stubBookRepository = GetBookRepositoryStub();
			var stubAuthorRepository = GetAuthorsRepositoryStub();
			var stubGenresRepository = GetGenresRepositoryStub();
			var unitOfWork = Mock.Of<IUnitOfWork>(x => x.BookRepository == stubBookRepository.Object 
													&& x.AuthorRepository == stubAuthorRepository.Object
													&& x.GenreRepository == stubGenresRepository.Object);

			BooksService = new BooksService(unitOfWork);
			AuthorsService = new AuthorsService(unitOfWork);
			GenresService = new GenresService(unitOfWork);
		}

		private Mock<IGenresRepository> GetGenresRepositoryStub()
		{
			var stub = new Mock<IGenresRepository>();
			stub.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Genre, bool>>>>(),
					It.IsAny<Func<IQueryable<Genre>, IOrderedQueryable<Genre>>>(),
					It.IsAny<string>()))
				.ReturnsAsync((IEnumerable<Expression<Func<Genre, bool>>> filters,
					Func<IQueryable<Genre>, IOrderedQueryable<Genre>> order, string includeProperties) =>
				{
					IEnumerable<Genre> localEntities = Genres;
					return GetAllStub(localEntities, filters, order);
				});

			stub.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) => Genres.SingleOrDefault(x => x.Id == id));

			stub.Setup(x => x.Create(It.IsAny<Genre>()))
				.Returns((Genre x) =>
				{
					Genres.Add(x);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var genre = Genres.SingleOrDefault(x => x.Id == id);
					if (genre == null) return false;

					Genres.Remove(genre);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<Genre>()))
				.Returns<Genre>(x =>
				{
					Genres.Remove(x);
					return true;
				});
			stub.Setup(x => x.GetTree(It.IsAny<IList<Expression<Func<Genre, bool>>>>()))
				.ReturnsAsync((IList<Expression<Func<Genre, bool>>> filters) => GetAllStub(Genres, filters, null));
			stub.Setup(x => x.Update(It.IsAny<Genre>())).Returns((Genre dbentity) =>
			{
				var genre = Genres.Single(x => x.Id == dbentity.Id);
				genre.Name = dbentity.Name;

				return true;
			});
			return stub;
		}

		private Mock<IGenericRepository<Author>> GetAuthorsRepositoryStub()
		{
			var stubAuthorRepository = new Mock<IGenericRepository<Author>>();
			stubAuthorRepository.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Author, bool>>>>(),
					It.IsAny<Func<IQueryable<Author>, IOrderedQueryable<Author>>>(),
					It.IsAny<string>()))
				.ReturnsAsync((IEnumerable<Expression<Func<Author, bool>>> filters,
					Func<IQueryable<Author>, IOrderedQueryable<Author>> order, string includeProperties) =>
				{
					IEnumerable<Author> localEntities = Authors;
					return GetAllStub(localEntities, filters, order);
				});

			stubAuthorRepository.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) => Authors.SingleOrDefault(x => x.Id == id));

			stubAuthorRepository.Setup(x => x.Create(It.IsAny<Author>()))
				.Returns((Author x) =>
				{
					Authors.Add(x);
					return true;
				});
			stubAuthorRepository.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var author = Authors.SingleOrDefault(x => x.Id == id);
					if (author == null) return false;

					Authors.Remove(author);
					return true;
				});
			stubAuthorRepository.Setup(x => x.Delete(It.IsAny<Author>()))
				.Returns<Author>(x =>
				{
					Authors.Remove(x);
					return true;
				});
			stubAuthorRepository.Setup(x => x.Update(It.IsAny<Author>())).Returns((Author dbentity) =>
			{
				var author = Authors.Single(x => x.Id == dbentity.Id);
				author.Lastname = dbentity.Lastname;
				author.Firstname = dbentity.Firstname;
				author.Middlename = dbentity.Middlename;

				return true;
			});
			return stubAuthorRepository;
		}

		private Mock<IGenericRepository<Book>> GetBookRepositoryStub()
		{
			var stubBookRepository = new Mock<IGenericRepository<Book>>();

			stubBookRepository.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Book, bool>>>>(),
					It.IsAny<Func<IQueryable<Book>, IOrderedQueryable<Book>>>(),
					It.IsAny<string>()))
				.ReturnsAsync((IEnumerable<Expression<Func<Book, bool>>> filters,
					Func<IQueryable<Book>, IOrderedQueryable<Book>> order, string includeProperties) =>
				{
					IEnumerable<Book> books = Books;
					return GetAllStub(books, filters, order);
				});

			stubBookRepository.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) => Books.SingleOrDefault(x => x.Id == id));

			stubBookRepository.Setup(x => x.Create(It.IsAny<Book>()))
				.Returns((Book x) =>
				{
					Books.Add(x);
					return true;
				});
			stubBookRepository.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var book = Books.SingleOrDefault(x => x.Id == id);
					if (book == null)
					{
						return false;
					}
					Books.Remove(book);
					return true;
				});
			stubBookRepository.Setup(x => x.Delete(It.IsAny<Book>()))
				.Returns<Book>(x =>
				{
					Books.Add(x);
					return true;
				});
			stubBookRepository.Setup(x => x.Update(It.IsAny<Book>())).Returns((Book dbentity) => true);
			return stubBookRepository;
		}


		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			AutoMapperConfig.Initialize();
		}

		private IEnumerable<TEntity> GetAllStub<TEntity>(IEnumerable<TEntity> entities, IEnumerable<Expression<Func<TEntity, bool>>> filters,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order) where TEntity : Entity
		{

			if (filters != null)
			{
				foreach (var expression in filters)
				{
					entities = entities.Where(expression.Compile());
				}
			}
			return order?.Invoke(entities.AsQueryable()) ?? entities;
		}
	}
}