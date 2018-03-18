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
using Library.Tests.Stubs;
using Moq;
using NUnit.Framework;

namespace Library.Tests.Services
{
	[TestFixture]
	abstract class ServiceTestsBase
	{
		#region protected services

		protected IBooksService BooksService;
		protected IAuthorsService AuthorsService;
		protected IGenresService GenresService;
		protected IPublishersService PublishersService;
		protected IInvoicesService InvoicesService;
		protected ISubscribersService SubscribersService;
		protected IRentsService RentsService;

		#endregion


		#region protected collections

		protected Collection<Book> Books;
		protected Collection<Author> Authors;
		protected Collection<Genre> Genres;
		protected Collection<Publisher> Publishers;
		protected Collection<Invoice> Invoices;
		protected Collection<Subscriber> Subscribers;
		protected Collection<Rent> Rents;

		#endregion


		protected Random Random = new Random();
		private static volatile bool _alreadyInitialized;

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

			var stubBookRepository = new BookRepositoryStub(Books).Get();
			var stubAuthorRepository = new AuthorRepositoryStub(Authors).Get();
			var stubGenresRepository = GetGenresRepositoryStub();
			var stubPublishersRepository = new PublishersRepositoryStub(Publishers).Get();
			var stubInvoicesRepository = new InvoicesRepositoryStub(Invoices).Get();
			var stubSubscribersRepository = new SubscribersRepositoryStub(Subscribers).Get();
			var stubRentsRepository = new RentsRepositoryStub(Rents).Get();
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

		private Mock<IGenresRepository> GetGenresRepositoryStub()
		{
			var stub = new Mock<IGenresRepository>();
			stub.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Genre, bool>>>>(),
					It.IsAny<Func<IQueryable<Genre>, IOrderedQueryable<Genre>>>(),
					It.IsAny<string>(), 0, null))
				.ReturnsAsync((IEnumerable<Expression<Func<Genre, bool>>> filters,
					Func<IQueryable<Genre>, IOrderedQueryable<Genre>> order, string includeProperties, int skip, int? take) =>
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