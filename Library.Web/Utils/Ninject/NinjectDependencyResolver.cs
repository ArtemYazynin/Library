﻿using System.Web.Http.Dependencies;
using Ninject;

namespace Library.Web.Utils.Ninject
{
	public class NinjectDependencyResolver: NinjectDependencyScope, IDependencyResolver, System.Web.Mvc.IDependencyResolver
	{
		private readonly IKernel _kernel;

		public NinjectDependencyResolver(IKernel kernel) : base(kernel)
		{
				_kernel = kernel;
			}

			public IDependencyScope BeginScope()
			{
				return new NinjectDependencyScope(_kernel.BeginBlock());
			}
	}
}