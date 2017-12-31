using System;
using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Genres
	{
		private static readonly Random Rnd = new Random();

		static Genres()
		{
			ComputersAndTecnology = new Genre("Computers & Technology")
			{
				Id = Rnd.Next(int.MaxValue)
			};
			Programming = new Genre("Programming")
			{
				Id = Rnd.Next(int.MaxValue),
				Version = new byte[] { 0, 0, 0, 0, 0, 0, 0, 120 },
				Parent = ComputersAndTecnology,

			};
			LanguageAndTools = new Genre("Languages & Tools")
			{
				Id = Rnd.Next(int.MaxValue),
				Parent = Programming,
			};
			CSharp = new Genre("C#")
			{
				Id = Rnd.Next(int.MaxValue),
				Parent = LanguageAndTools
			};
			MicrosoftProgramming = new Genre("Microsoft Programming")
			{
				Id = Rnd.Next(int.MaxValue),
				Parent = Programming,
			};
			DotNet = new Genre(".NET")
			{
				Id = Rnd.Next(int.MaxValue),
				Parent = MicrosoftProgramming
			};
			WebProgramming = new Genre("Web Programming")
			{
				Id = Rnd.Next(int.MaxValue),
				Parent = Programming
			};
			JavaScript = new Genre("JavaScript")
			{
				Id = Rnd.Next(int.MaxValue),
				Parent = WebProgramming
			};

			ComputersAndTecnology.AddChild(Programming);

			Programming.AddChild(WebProgramming);
			Programming.AddChild(MicrosoftProgramming);
			Programming.AddChild(LanguageAndTools);

			LanguageAndTools.AddChild(CSharp);
			MicrosoftProgramming.AddChild(DotNet);
			WebProgramming.AddChild(JavaScript);
		}
		public static Genre ComputersAndTecnology { get; }
		public static Genre Programming { get; }

		public static Genre WebProgramming { get; }
		public static Genre JavaScript { get; }


		public static Genre MicrosoftProgramming { get; }

		public static Genre DotNet { get; }

		public static Genre LanguageAndTools { get; }

		public static Genre CSharp { get; }
	}
}
