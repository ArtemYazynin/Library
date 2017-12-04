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
		protected IPublishersService PublishersService;
		protected IInvoicesService InvoicesService;

		protected Collection<Book> Books;
		protected Collection<Author> Authors;
		protected Collection<Genre> Genres;
		protected Collection<Publisher> Publishers;
		protected Collection<Invoice> Invoices;

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
			Publishers = new Collection<Publisher>()
			{
				DefaultData.Publishers.Self,
				DefaultData.Publishers.DmkPress,
				DefaultData.Publishers.Piter,
				DefaultData.Publishers.SymbolPlus,
				DefaultData.Publishers.Viliams,

			};
			Invoices = new Collection<Invoice>()
			{
				DefaultData.Invoices.First,
				DefaultData.Invoices.Second,
				DefaultData.Invoices.Third
			};
			var stubBookRepository = GetBookRepositoryStub();
			var stubAuthorRepository = GetAuthorsRepositoryStub();
			var stubGenresRepository = GetGenresRepositoryStub();
			var stubPublishersRepository = GetPublishersRepositoryStub();
			var stubInvoicesRepository = GetInvoicesRepositoryStub();
			var unitOfWork = Mock.Of<IUnitOfWork>(x => x.BookRepository == stubBookRepository.Object 
													&& x.AuthorRepository == stubAuthorRepository.Object
													&& x.GenreRepository == stubGenresRepository.Object
													&& x.PublisherRepository == stubPublishersRepository.Object
													&& x.InvoiceRepository == stubInvoicesRepository.Object);

			BooksService = new BooksService(unitOfWork);
			AuthorsService = new AuthorsService(unitOfWork);
			GenresService = new GenresService(unitOfWork);
			PublishersService = new PublishersService(unitOfWork);
			InvoicesService = new InvoicesService(unitOfWork);
		}
		private Mock<IGenericRepository<Invoice>> GetInvoicesRepositoryStub()
		{
			var stub = new Mock<IGenericRepository<Invoice>>();
			stub.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Invoice, bool>>>>(),
										It.IsAny<Func<IQueryable<Invoice>, IOrderedQueryable<Invoice>>>(),
										It.IsAny<string>()))
				.ReturnsAsync((IEnumerable<Expression<Func<Invoice, bool>>> filters,
					Func<IQueryable<Invoice>, IOrderedQueryable<Invoice>> orders
					, string includeProperties) => GetAllStub(Invoices, filters, orders));

			stub.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) =>
				{
					return Invoices.SingleOrDefault(x => x.Id == id);
				});
			stub.Setup(x => x.Create(It.IsAny<Invoice>()))
				.Returns((Invoice x) =>
				{
					Invoices.Add(x);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var deletedInvoice = Invoices.SingleOrDefault(x => x.Id == id);
					if (deletedInvoice == null)
					{
						return false;
					}

					Invoices.Remove(deletedInvoice);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<Invoice>()))
				.Returns((Invoice x) =>
				{
					try
					{
						Invoices.Remove(x);
						return true;
					}
					catch (Exception)
					{

						return false;
					}
				});
			stub.Setup(x => x.Update(It.IsAny<Invoice>()))
				.Returns((Invoice x) =>
				{
					try
					{
						var invoice = Invoices.Single(n => n.Id == x.Id);
						invoice.Date = x.Date;
						invoice.IncomingBooks = x.IncomingBooks;

						return true;
					}
					catch (Exception)
					{
						return false;
					}
				});
			return stub;
		}

		private Mock<IGenericRepository<Publisher>> GetPublishersRepositoryStub()
		{
			var stub = new Mock<IGenericRepository<Publisher>>();
			stub.Setup(x=>x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Publisher, bool>>>>(),
										It.IsAny<Func<IQueryable<Publisher>, IOrderedQueryable<Publisher>>>(),
										It.IsAny<string>()))
				.ReturnsAsync((IEnumerable<Expression<Func<Publisher, bool>>> filters,
					Func<IQueryable<Publisher>, IOrderedQueryable<Publisher>> orders
					, string includeProperties) => GetAllStub(Publishers, filters,orders));

			stub.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) =>
				{
					return Publishers.SingleOrDefault(x => x.Id == id);
				});
			stub.Setup(x => x.Create(It.IsAny<Publisher>()))
				.Returns((Publisher x) =>
				{
					Publishers.Add(x);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var deletedPublisher = Publishers.SingleOrDefault(x => x.Id == id);
					if (deletedPublisher == null)
					{
						return false;
					}

					Publishers.Remove(deletedPublisher);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<Publisher>()))
				.Returns((Publisher x) =>
				{
					try
					{
						Publishers.Remove(x);
						return true;
					}
					catch (Exception)
					{
						
						return false;
					}
				});
			stub.Setup(x => x.Update(It.IsAny<Publisher>()))
				.Returns((Publisher x) =>
				{
					try
					{
						var publisher = Publishers.Single(n => n.Id == x.Id);
						publisher.Name = x.Name;

						return true;
					}
					catch (Exception)
					{
						return false;
					}
				});
			return stub;
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

		private static volatile bool _alreadyInitialized;
		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			if (!_alreadyInitialized)
			{
				AutoMapperConfig.Initialize();
				_alreadyInitialized = true;
			}
			
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