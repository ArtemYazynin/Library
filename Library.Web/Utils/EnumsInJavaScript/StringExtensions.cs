namespace Library.Web.Utils.EnumsInJavaScript
{
	public static class StringExtensions
	{
		public static string ToCamelCase(this string s)
		{
			return s.Substring(0, 1).ToLower() + s.Substring(1);
		}
	}
}