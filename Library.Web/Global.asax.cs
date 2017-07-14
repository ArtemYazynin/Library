using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Library.Services.Impls;
using Library.Web.Utils;
using Ninject;
using Ninject.Modules;

namespace Library.Web
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AutoMapperConfig.Initialize();

			AreaRegistration.RegisterAllAreas();

			#region ninject registration

			NinjectModule registrations = new NinjectRegistrations();
			var kernel = new StandardKernel(registrations);
			var ninjectResolver = new NinjectDependencyResolver(kernel);
			DependencyResolver.SetResolver(ninjectResolver); // MVC
			GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver; // Web API

			#endregion

			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
