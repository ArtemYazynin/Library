using System.Web.Http;
using Library.Web.Controllers.api;

namespace Library.Web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
