using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Library.Web.Utils.EnumsInJavaScript
{
	public static class JavascriptEnabledEnums
	{
		private static IList<JavascriptEnumTypeInfo> _types = null;

		public static void LoadTypes()
		{
			if (_types != null) return;
			_types = new List<JavascriptEnumTypeInfo>();
			var typesWithAttribute =
				from t in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsEnum)
				let attributes = t.GetCustomAttributes(typeof(JavascriptEnumAttribute), true)
				where attributes != null && attributes.Length > 0
				select new { Type = t, Attributes = attributes.Cast<JavascriptEnumAttribute>() };

			foreach (var info in typesWithAttribute.Select(x => new { x.Type, x.Attributes.First().Groups }))
			{
				// if there are no groups then it defaults to everywhere
				if (info.Groups == null || info.Groups.Length == 0)
				{
					_types.Add(new JavascriptEnumTypeInfo
					{
						Type = info.Type,
						Group = ""
					});
				}
				else
				{
					foreach (var group in info.Groups)
					{
						_types.Add(new JavascriptEnumTypeInfo
						{
							Type = info.Type,
							Group = string.IsNullOrEmpty(group) ? "" : group.ToLower()
						});
					}
				}

			}
		}

		public static Type[] GetTypes(string group)
		{
			LoadTypes();
			return _types
				.Where(x => string.IsNullOrEmpty(group) || x.Group == group.ToLower() || string.IsNullOrEmpty(x.Group))
				.Select(x => x.Type)
				.Distinct()
				.ToArray();
		}
	}

	internal class JavascriptEnumTypeInfo
	{
		public string Group { get; set; }
		public Type Type { get; set; }
	}
}