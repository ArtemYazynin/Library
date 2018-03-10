using System;

namespace Library.Web.Utils.EnumsInJavaScript
{
	public class JavascriptEnumAttribute : Attribute
	{
		public string[] Groups { get; set; }

		public JavascriptEnumAttribute(params string[] groups)
		{
			Groups = groups;
		}
	}
}