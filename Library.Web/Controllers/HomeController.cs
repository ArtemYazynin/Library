using System.Web.Mvc;
using Library.Services;
using Library.Services.Services;

namespace Library.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IBooksService _booksService;
		public HomeController(IBooksService booksService)
		{
			_booksService = booksService;
		}

		public ActionResult Index()
		{
			return View();
		}
	}
}
