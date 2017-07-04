using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Library.Services;
using Library.Services.Impls;
using Ninject;

namespace Library.Web.Utils
{
	public class NinjectDependencyMvcResolver:IDependencyResolver
	{
		private readonly IKernel _kernel;
		public NinjectDependencyMvcResolver()
		{
			_kernel = new StandardKernel();
			AddBindings();
		}
		public object GetService(Type serviceType)
		{
			return _kernel.TryGet(serviceType);
		}
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _kernel.GetAll(serviceType);
		}
		private void AddBindings()
		{
			_kernel.Bind<IAuthorsService>().To<AuthorsService>();
			_kernel.Bind<IBooksService>().To<BooksService>();
		}
	}
}