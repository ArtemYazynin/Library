using System;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Authors
	{
		private static readonly Random Rnd = new Random();

		public static Author Yazynin = new Author("Язынин", "Артем", "Дмитриевич")
		{
			Id = Rnd.Next(int.MaxValue)
		};

		public static Author Flenagan = new Author("Флэнаган", "Дэвид")
		{
			Id = Rnd.Next(int.MaxValue),
		};
		public static Author Rezig = new Author("Резиг", "Джон") { Id = Rnd.Next(int.MaxValue) };
		public static Author Ferguson = new Author("Фергюсон", "Расс") { Id = Rnd.Next(int.MaxValue)};
		public static Author Zakas = new Author("Закас", "Николас") { Id = Rnd.Next(int.MaxValue)};
		public static Author Simpson = new Author("Симпсон", "Кайл") { Id = Rnd.Next(int.MaxValue)};
		public static Author Rihter = new Author("Рихтер", "Джеффри") { Id = Rnd.Next(int.MaxValue) };
		public static Author Shildt = new Author("Шилдт", "Герберт") { Id = Rnd.Next(int.MaxValue)};
		public static Author Troelsen = new Author("Троелсен", "Эндрю") { Id = Rnd.Next(int.MaxValue) };
		public static Author Jepkins = new Author("Джепикс", "Филипп") { Id = Rnd.Next(int.MaxValue) };
		public static Author Devis = new Author("Дэвис", "Алекс") { Id = Rnd.Next(int.MaxValue) };
	}
}