using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Library.Web.Utils.EnumsInJavaScript;

namespace Library.Web.Controllers
{
	[RoutePrefix("enums")]
	[Route("")]
	public class EnumsController : Controller
	{
		[OutputCache(Duration = int.MaxValue, Location = OutputCacheLocation.Any, VaryByParam = "group")]
		[Route("{group?}")]
		public ContentResult Get(string group = null)
		{
			var contentParts = JavascriptEnabledEnums.GetTypes(group).Select(type => ConvertEnumToJson(type)).ToList();
			var javascript = $"var Enums = {{ {string.Join(",", contentParts)} }};";

			return Content(javascript, "text/javascript");
		}

		public static string ConvertEnumToJson(Type e, string varName = null)
		{
			if (varName == null)
			{
				varName = e.Name.ToCamelCase();
			}

			var ret = varName + ": {";
			foreach (var val in Enum.GetValues(e))
			{
				ret += Enum.GetName(e, val).ToCamelCase() + ":" + ((int)val) + ",";
			}
			ret += "}";
			return ret;
		}
	}
}