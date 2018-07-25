using System;

namespace Library.Common
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