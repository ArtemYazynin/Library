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
			Viliams = new Publisher("Вильямс")
			{
				Id = Rnd.Next(int.MaxValue)
			};
			Self = new Publisher("Язынин Артем Дмитриевич")
			{
				Id = Rnd.Next(int.MaxValue),
			};
			Piter = new Publisher("Питер")
			{
				Id = Rnd.Next(int.MaxValue),

			};
			DmkPress = new Publisher("ДМК Пресс")
			{
				Id = Rnd.Next(int.MaxValue),
			};
			SymbolPlus = new Publisher("Символ-Плюс")
			{
				Id = Rnd.Next(int.MaxValue),
			};
		}

		public static Publisher Viliams { get; }
		public static Publisher Self { get; }
		public static Publisher Piter { get; }
		public static Publisher DmkPress { get; }
		public static Publisher SymbolPlus { get; }
	}
}
