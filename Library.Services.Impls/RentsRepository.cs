using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	public class RentsRepository: GenericBaseRepository<Rent>
	{
		public RentsRepository(LibraryContext context) : base(context)
		{
		}

		public override bool Create(Rent entity)
		{
			Context.Books.Attach(entity.Book);
			Context.Subscribers.Attach(entity.Subscriber);
			DbSet.Add(entity);
			return true;
		}
	}
}