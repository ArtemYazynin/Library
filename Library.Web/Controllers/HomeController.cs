using System.Threading.Tasks;
using System.Web.Mvc;
using Library.Services;

namespace Library.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IAuthorsService _authorsService;
		private readonly IBooksService _booksService;
		public HomeController(IAuthorsService authorsService, IBooksService booksService)
		{
			_authorsService = authorsService;
			_booksService = booksService;
		}

		public async Task<ActionResult> Index()
		{
			var books = await _booksService.Get();
			return View(books);
		}
	}
}
