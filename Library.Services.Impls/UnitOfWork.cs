using System;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	public class UnitOfWork: IUnitOfWork
	{

		#region private fields
		private readonly LibraryContext _context = new LibraryContext();

		private GenericRepository<Author> _authorRepository;
		private GenericRepository<Book> _bookRepository;
		private GenericRepository<Edition> _editionRepository;
		private GenericRepository<File> _fileRepository;
		private GenericRepository<Genre> _genreRepository;
		private GenericRepository<Invoice> _invoiceRepository;
		private GenericRepository<Publisher> _publisherRepository;
		private GenericRepository<Rent> _rentRepository;
		private GenericRepository<Subscriber> _subscriberRepository;

		#endregion


		public IGenericRepository<Author> AuthorRepository => _authorRepository ?? (_authorRepository = new GenericRepository<Author>(_context));
		public IGenericRepository<Book> BookRepository => _bookRepository ?? (_bookRepository = new GenericRepository<Book>(_context));
		public IGenericRepository<Edition> EditionRepository => _editionRepository ?? (_editionRepository = new GenericRepository<Edition>(_context));
		public IGenericRepository<File> FileRepository => _fileRepository ?? (_fileRepository = new GenericRepository<File>(_context));
		public IGenericRepository<Genre> GenreRepository => _genreRepository ?? (_genreRepository = new GenericRepository<Genre>(_context));
		public IGenericRepository<Invoice> InvoiceRepository => _invoiceRepository ?? (_invoiceRepository = new GenericRepository<Invoice>(_context));
		public IGenericRepository<Publisher> PublisherRepository => _publisherRepository ?? (_publisherRepository = new GenericRepository<Publisher>(_context));
		public IGenericRepository<Rent> RentRepository => _rentRepository ?? (_rentRepository = new GenericRepository<Rent>(_context));
		public IGenericRepository<Subscriber> SubscriberRepository => _subscriberRepository ?? (_subscriberRepository = new GenericRepository<Subscriber>(_context));

		public void Save()
		{
			_context.SaveChanges();
		}

		#region disposing

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

	}
}