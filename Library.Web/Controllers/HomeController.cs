using System.Web.Mvc;
using Library.Services;

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
			var books = _booksService.GetAll();
			return View(books);
		}
	}
}
