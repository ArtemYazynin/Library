using System;
using System.Threading.Tasks;
using Library.ObjectModel.Models;

namespace Library.Services
{
	public interface IUnitOfWork: IDisposable
	{
		IGenericRepository<Author> AuthorRepository { get; }
		IGenericRepository<Book> BookRepository { get; }
		IGenericRepository<Edition> EditionRepository { get; }
		IGenericRepository<File> FileRepository { get; }
		IGenresRepository GenreRepository { get; }
		IGenericRepository<Invoice> InvoiceRepository { get; }
		IGenericRepository<Publisher> PublisherRepository { get; }
		IGenericRepository<Rent> RentRepository { get; }
		IGenericRepository<Subscriber> SubscriberRepository { get; }
		Task<int> Save();
	}
}