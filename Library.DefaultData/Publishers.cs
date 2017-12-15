using System;
using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Publishers
	{
		private static readonly Random Rnd = new Random();

		static Publishers()
		{
			Viliams = new Publisher()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Вильямс",

			};
			Self = new Publisher()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Язынин Артем Дмитриевич",

			};
			Piter = new Publisher()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Питер",

			};
			DmkPress = new Publisher()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "ДМК Пресс",
				
			};
			SymbolPlus = new Publisher()
			{
				Id = Rnd.Next(int.MaxValue),
				Name = "Символ-Плюс",
			};
		}

		public static Publisher Viliams { get; }
		public static Publisher Self { get; }
		public static Publisher Piter { get; }
		public static Publisher DmkPress { get; }
		public static Publisher SymbolPlus { get; }
	}
}
