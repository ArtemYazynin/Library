using System.Data.Entity;
using Library.Services;
using Library.Services.Impls;
using Library.Services.Impls.Services;
using Library.Services.Services;
using Ninject.Modules;

namespace Library.Web.Utils
{
	public class NinjectRegistrations : NinjectModule
	{
		public override void Load()
		{
			Bind<DbContext>().To<LibraryContext>();
			Bind<IUnitOfWork>().To<UnitOfWork>();
			Bind<IEditionsService>().To<EditionsService>();
			Bind<IPublishersService>().To<PublishersService>();
			Bind<IAuthorsService>().To<AuthorsService>();
			Bind<IGenresService>().To<GenresService>();
			Bind<IBooksService>().To<BooksService>();
			Bind<IInvoicesService>().To<InvoicesService>();
			Bind<ISubscribersService>().To<SubscribersService>();
		}
	}
}