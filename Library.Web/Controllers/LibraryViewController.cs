using System.Web.Mvc;

namespace Library.Web.Controllers
{
	public class LibraryViewController: Controller
	{
		public ActionResult Books()
		{
			return PartialView("..//Books");
		}

		public ActionResult BookDetails()
		{
			return PartialView("..//BookDetails");
		}
	}
}