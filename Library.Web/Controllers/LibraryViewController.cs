﻿using System.Web.Mvc;

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
		public ActionResult Publishers()
		{
			return PartialView("..//Publishers");
		}

		public ActionResult Invoices()
		{
			return PartialView("..//Invoices");
		}

		public ActionResult InvoiceDetails()
		{
			return PartialView("..//InvoiceDetails");
		}

		public ActionResult Subscribers()
		{
			return PartialView("..//Subscribers");
		}

		public ActionResult Rents()
		{
			return PartialView("..//Rents");
		}

		public ActionResult MostPopular()
		{
			return PartialView("..//MostPopular");
		}

		public ActionResult Statistics()
		{
			return PartialView("..//Statistics");
		}
	}
}