using System.Threading.Tasks;
using System.Web.Mvc;
using Library.Services;

namespace Library.Web.Controllers
{
	public class HomeController : Controller
	{
		public async Task<ActionResult> Index()
		{
			return View();
		}
	}
}
