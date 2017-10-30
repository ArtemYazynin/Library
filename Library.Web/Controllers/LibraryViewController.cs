using System.Web.Mvc;

namespace Library.Web.Controllers
{
	public class LibraryViewController: Controller
	{
		public ActionResult Books()
		{
			return PartialView("..//Books");
		}

		public ActionResult Authors()
		{
			return PartialView("..//Authors");
		}

		public ActionResult Genres()
		{
			return PartialView("..//Genres");
		}

		public ActionResult BookDetails()
		{
			return PartialView("..//BookDetails");
		}

		public ActionResult AuthorDetails()
		{
			return PartialView("..//AuthorDetails");
		}
	}
}