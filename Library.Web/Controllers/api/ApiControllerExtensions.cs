using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Library.Web.Controllers.api
{
	public static class ApiControllerExtensions
	{
		public static async Task AddTotalItemsInHeader(this ApiController apiController, Func<Task<long>> countFunc)
		{
			var totalItems = await countFunc();
			HttpContext.Current.Response.Headers.Add("totalItems", totalItems.ToString());
		}
	}
}