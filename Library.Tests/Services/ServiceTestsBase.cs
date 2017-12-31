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
		protected ISubscribersService SubscribersService;
		protected IRentsService RentsService;

		protected Collection<Book> Books;
		protected Collection<Author> Authors;
		protected Collection<Genre> Genres;
		protected Collection<Publisher> Publishers;
		protected Collection<Invoice> Invoices;
		protected Collection<Subscriber> Subscribers;
		protected Collection<Rent> Rents;

		protected Random Random = new Random();

		[SetUp]
		public void SetUp()
		{
			Books = new Collection<Book>
			{
				DefaultData.Books.JsPocketGuide,
				DefaultData.Books.JsForProfessionals,
				DefaultData.Books.JsOptimizingPerfomance,
				DefaultData.Books.Es6AndNotOnly,
				DefaultData.Books.ClrVia,
				DefaultData.Books.MyEvernoteNotes,
				DefaultData.Books.WithoutAuthorsBook,
				DefaultData.Books.CSharpCompleteGuide,
				DefaultData.Books.CSharp6AndNetPlatform,
				DefaultData.Books.AsyncProgrammingCSharp5
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

			#region Publishers

			DefaultData.Publishers.Viliams.Books = new List<Book>()
			{
				DefaultData.Books.JsPocketGuide,
				DefaultData.Books.JsForProfessionals,
				DefaultData.Books.CSharpCompleteGuide,
				DefaultData.Books.CSharp6AndNetPlatform
			};

			DefaultData.Publishers.Self.Books = new List<Book>()
			{
				DefaultData.Books.WithoutAuthorsBook,
				DefaultData.Books.MyEvernoteNotes
			};
			DefaultData.Publishers.Piter.Books = new List<Book>()
			{
				DefaultData.Books.Es6AndNotOnly,
				DefaultData.Books.ClrVia
			};
			
			DefaultData.Publishers.DmkPress.Books = new List<Book>() { DefaultData.Books.AsyncProgrammingCSharp5 };


			DefaultData.Publishers.SymbolPlus.Books = new List<Book>() { DefaultData.Books.JsOptimizingPerfomance };
			Publishers = new Collection<Publisher>()
			{
				DefaultData.Publishers.Self,
				DefaultData.Publishers.DmkPress,
				DefaultData.Publishers.Piter,
				DefaultData.Publishers.SymbolPlus,
				DefaultData.Publishers.Viliams,

			};

			#endregion

			Invoices = new Collection<Invoice>()
			{
				DefaultData.Invoices.First,
				DefaultData.Invoices.Second,
				DefaultData.Invoices.Third
			};

			Subscribers = new Collection<Subscriber>()
			{
				DefaultData.Subscribers.Petrov,
				DefaultData.Subscribers.Ivanov,
				DefaultData.Subscribers.Maslov,
				DefaultData.Subscribers.Sidorov
			};
			Rents = new Collection<Rent>()
			{
				DefaultData.Rents.RentIvanov1,
				DefaultData.Rents.RentIvanov2,
				DefaultData.Rents.RentIvanov3,
				DefaultData.Rents.RentMaslov,
				DefaultData.Rents.RentPetrov,
				DefaultData.Rents.RentSidorov
			};

			var stubBookRepository = GetBookRepositoryStub();
			var stubAuthorRepository = GetAuthorsRepositoryStub();
			var stubGenresRepository = GetGenresRepositoryStub();
			var stubPublishersRepository = GetPublishersRepositoryStub();
			var stubInvoicesRepository = GetInvoicesRepositoryStub();
			var stubSubscribersRepository = GetSubscribersRepositoryStub();
			var stubRentsRepository = GetRentsRepositoryStub();
			var unitOfWork = Mock.Of<IUnitOfWork>(x => x.BookRepository == stubBookRepository.Object 
													&& x.AuthorRepository == stubAuthorRepository.Object
													&& x.GenreRepository == stubGenresRepository.Object
													&& x.PublisherRepository == stubPublishersRepository.Object
													&& x.InvoiceRepository == stubInvoicesRepository.Object
													&& x.SubscriberRepository == stubSubscribersRepository.Object
													&& x.RentRepository == stubRentsRepository.Object);

			BooksService = new BooksService(unitOfWork);
			AuthorsService = new AuthorsService(unitOfWork);
			GenresService = new GenresService(unitOfWork);
			PublishersService = new PublishersService(unitOfWork);
			InvoicesService = new InvoicesService(unitOfWork);
			SubscribersService = new SubscribersService(unitOfWork);
			RentsService = new RentsService(unitOfWork);
		}
		private Mock<IGenericRepository<Rent>> GetRentsRepositoryStub()
		{
			var stub = new Mock<IGenericRepository<Rent>>();
			stub.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Rent, bool>>>>(),
										It.IsAny<Func<IQueryable<Rent>, IOrderedQueryable<Rent>>>(),
										It.IsAny<string>()))
				.ReturnsAsync((IEnumerable<Expression<Func<Rent, bool>>> filters,
					Func<IQueryable<Rent>, IOrderedQueryable<Rent>> orders
					, string includeProperties) => GetAllStub(Rents, filters, orders));

			stub.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) =>
				{
					return Rents.SingleOrDefault(x => x.Id == id);
				});
			stub.Setup(x => x.Create(It.IsAny<Rent>()))
				.Returns((Rent x) =>
				{
					Rents.Add(x);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var deletedRent = Rents.SingleOrDefault(x => x.Id == id);
					if (deletedRent == null)
					{
						return false;
					}

					Rents.Remove(deletedRent);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<Rent>()))
				.Returns((Rent x) =>
				{
					try
					{
						Rents.Remove(x);
						return true;
					}
					catch (Exception)
					{

						return false;
					}
				});
			stub.Setup(x => x.Update(It.IsAny<Rent>()))
				.Returns((Rent x) =>
				{
					try
					{
						var rent = Rents.Single(n => n.Id == x.Id);
						rent.IsActive = x.IsActive;

						return true;
					}
					catch (Exception)
					{
						return false;
					}
				});
			return stub;
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

		private Mock<IGenericRepository<Subscriber>> GetSubscribersRepositoryStub()
		{
			var stubSubscriberRepository = new Mock<IGenericRepository<Subscriber>>();
			stubSubscriberRepository.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Subscriber, bool>>>>(),
					It.IsAny<Func<IQueryable<Subscriber>, IOrderedQueryable<Subscriber>>>(),
					It.IsAny<string>()))
				.ReturnsAsync((IEnumerable<Expression<Func<Subscriber, bool>>> filters,
					Func<IQueryable<Subscriber>, IOrderedQueryable<Subscriber>> order, string includeProperties) =>
				{
					IEnumerable<Subscriber> localEntities = Subscribers;
					return GetAllStub(localEntities, filters, order);
				});

			stubSubscriberRepository.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) => Subscribers.SingleOrDefault(x => x.Id == id));

			stubSubscriberRepository.Setup(x => x.Create(It.IsAny<Subscriber>()))
				.Returns((Subscriber x) =>
				{
					Subscribers.Add(x);
					return true;
				});
			stubSubscriberRepository.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var subscriber = Subscribers.SingleOrDefault(x => x.Id == id);
					if (subscriber == null) return false;

					Subscribers.Remove(subscriber);
					return true;
				});
			stubSubscriberRepository.Setup(x => x.Delete(It.IsAny<Subscriber>()))
				.Returns<Subscriber>(x =>
				{
					Subscribers.Remove(x);
					return true;
				});
			stubSubscriberRepository.Setup(x => x.Update(It.IsAny<Subscriber>())).Returns((Subscriber dbentity) =>
			{
				var subscriber = Subscribers.Single(x => x.Id == dbentity.Id);
				subscriber.Lastname = dbentity.Lastname;
				subscriber.Firstname = dbentity.Firstname;
				subscriber.Middlename = dbentity.Middlename;

				return true;
			});
			return stubSubscriberRepository;
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