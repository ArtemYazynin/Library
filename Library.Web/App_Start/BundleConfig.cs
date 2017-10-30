using System.Web.Optimization;

namespace Library.Web
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/angular")
						.Include("~/Scripts/angular/angular.js")
						.IncludeDirectory("~/Scripts/angular/", "*.js"));

			bundles.Add(new ScriptBundle("~/bundles/viewScripts")
						.Include("~/Scripts/oi.select/select.min.js")
						.Include("~/Scripts/craftpip/angular-confirm.min.js")
						.Include("~/Scripts/angular-ui-tree/angular-ui-tree.min.js")
						.Include("~/Scripts/ViewScripts/Root/RootModule.js")
						.Include("~/Scripts/ViewScripts/Root/RootController.js")
						
						.IncludeDirectory("~/Scripts/ViewScripts/", "*Module.js",true)
						.IncludeDirectory("~/Scripts/ViewScripts/", "*Controller.js", true)
						.IncludeDirectory("~/Scripts/ViewScripts/", "*Service.js", true)
						.IncludeDirectory("~/Scripts/ViewScripts/", "*.js", true));

			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap/bootstrap.js",
					  "~/Scripts/bootstrap/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css",
					  "~/Content/select.min.css",
					  "~/Content/angular-confirm.min.css",
					  "~/Content/angular-ui-tree.min.css"));
		}
	}
}
