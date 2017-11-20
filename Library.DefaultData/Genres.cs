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
			ComputersAndTecnology = new Genre()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Computers & Technology",
				
			};
			Programming = new Genre()
			{
				Id = Rnd.Next(int.MaxValue),
				Version = new byte[] { 0, 0, 0, 0, 0, 0, 0, 120 },
				Name = "Programming",
				Parent = ComputersAndTecnology,

			};
			LanguageAndTools = new Genre()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Languages & Tools",
				Parent = Programming,
			};
			CSharp = new Genre()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "C#",
				Parent = LanguageAndTools
			};
			MicrosoftProgramming = new Genre()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Microsoft Programming",
				Parent = Programming,
			};
			DotNet = new Genre()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = ".NET",
				Parent = MicrosoftProgramming
			};
			WebProgramming = new Genre()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Web Programming",
				Parent = Programming
			};
			JavaScript = new Genre()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "JavaScript",
				Parent = WebProgramming
			};

			ComputersAndTecnology.Children = new List<Genre>() { Programming };
			Programming.Children = new List<Genre>()
			{
				WebProgramming,
				MicrosoftProgramming,
				LanguageAndTools
			};
			LanguageAndTools.Children = new List<Genre>()
			{
				CSharp
			};
			MicrosoftProgramming.Children = new List<Genre>()
			{
				DotNet
			};
			WebProgramming.Children = new List<Genre>()
			{
				JavaScript
			};
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
