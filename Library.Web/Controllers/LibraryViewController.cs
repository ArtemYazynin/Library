﻿using System.Web.Mvc;

namespace Library.Web.Controllers
{
	public class LibraryViewController: Controller
	{
		public ActionResult Books()
		{
			return PartialView("..//Books");
		}
	}
}