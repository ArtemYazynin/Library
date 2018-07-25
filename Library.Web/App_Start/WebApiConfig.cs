using System.Web.Http;
using Library.Web.Controllers.api;
using Newtonsoft.Json;

namespace Library.Web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();
			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling= ReferenceLoopHandling.Ignore;
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
