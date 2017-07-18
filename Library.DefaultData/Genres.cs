using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Genres
	{
		public static readonly Genre ComputersAndTecnology = new Genre() { Name = "Computers & Technology" };
		public static readonly Genre Programming = new Genre() { Name = "Programming", Parent = ComputersAndTecnology };

		public static readonly Genre WebProgramming = new Genre() { Name = "Web Programming", Parent = Programming };
		public static readonly Genre JavaScript = new Genre() { Name = "JavaScript", Parent = WebProgramming };


		public static readonly Genre MicrosoftProgramming = new Genre()
		{
			Name = "Microsoft Programming",
			Parent = Programming
		};

		public static readonly Genre DotNet = new Genre() { Name = ".NET", Parent = MicrosoftProgramming };

		public static readonly Genre LanguageAndTools = new Genre() { Name = "Languages & Tools", Parent = Programming };
		public static readonly Genre CSharp = new Genre() { Name = "C#", Parent = LanguageAndTools };
	}
}
