using System.Threading.Tasks;
using System.Web.Mvc;
using Library.Services;
using Library.Services.Impls;

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
			var books = _booksService.Get();
			return View(books);
		}
	}
}
