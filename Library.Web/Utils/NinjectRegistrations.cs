using Library.Services;
using Library.Services.Impls;
using Ninject.Modules;

namespace Library.Web.Utils
{
	public class NinjectRegistrations : NinjectModule
	{
		public override void Load()
		{
			
			Bind<IUnitOfWork>().To<UnitOfWork>();
			Bind<IAuthorsService>().To<AuthorsService>();
			Bind<IBooksService>().To<BooksService>();
		}
	}
}