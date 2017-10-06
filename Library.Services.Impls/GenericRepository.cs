using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	public class GenericRepository<TEntity> : GenericBaseRepository<TEntity> where TEntity : Entity
	{

		public GenericRepository(LibraryContext context):base(context)
		{
		}
	}
}