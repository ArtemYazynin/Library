using System;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Publishers
	{
		private readonly static Random Rnd = new Random();
		public static readonly Publisher Viliams = new Publisher()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "Вильямс"
		};
		public static readonly Publisher Self = new Publisher()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "Язынин Артем Дмитриевич"
		};
		public static readonly Publisher Piter = new Publisher()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "Питер"
		};
		public static readonly Publisher DmkPress = new Publisher()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "ДМК Пресс"
		};
		public static readonly Publisher SymbolPlus = new Publisher()
		{
			Id = Rnd.Next(int.MaxValue),
			Name = "Символ-Плюс"
		};
	}
}
