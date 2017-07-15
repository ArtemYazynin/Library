using System.Data.Entity;
using Library.Services;
using Library.Services.Impls;
using Ninject.Modules;

namespace Library.Web.Utils
{
	public class NinjectRegistrations : NinjectModule
	{
		public override void Load()
		{
			Bind<DbContext>().To<LibraryContext>();
			Bind<IUnitOfWork>().To<UnitOfWork>();
			Bind<IBooksService>().To<BooksService>();
		}
	}
}