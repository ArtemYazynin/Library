using System.Data.Entity;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	public class InvoicesRepository : GenericBaseRepository<Invoice>
	{
		public InvoicesRepository(LibraryContext context) : base(context)
		{
		}

		public override bool Create(Invoice entity)
		{
			foreach (var incomingBook in entity.IncomingBooks)
			{
				Context.Entry(incomingBook.Book).State = EntityState.Unchanged;
				//Context.Books.Attach(incomingBook.Book);
			}
			DbSet.Add(entity);
			return true;
		}
	}
}