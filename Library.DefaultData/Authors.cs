using System;
using System.Text;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Authors
	{
		private static readonly Random Rnd = new Random();

		public static Author Yazynin = new Author()
		{ Id = Rnd.Next(int.MaxValue), Lastname = "Язынин", Firstname = "Артем", Middlename = "Дмитриевич" };

		public static Author Flenagan = new Author()
		{
			Id = Rnd.Next(int.MaxValue),
			Version = Encoding.ASCII.GetBytes(Rnd.Next(int.MaxValue).ToString()),
			Lastname = "Флэнаган", 
			Firstname = "Дэвид"
		};
		public static Author Rezig = new Author() { Id = Rnd.Next(int.MaxValue), Version = Encoding.ASCII.GetBytes(Rnd.Next(int.MaxValue).ToString()), Lastname = "Резиг", Firstname = "Джон"};
		public static Author Ferguson = new Author() { Id = Rnd.Next(int.MaxValue), Version = Encoding.ASCII.GetBytes(Rnd.Next(int.MaxValue).ToString()), Lastname = "Фергюсон", Firstname = "Расс" };
		public static Author Zakas = new Author() { Id = Rnd.Next(int.MaxValue), Version = Encoding.ASCII.GetBytes(Rnd.Next(int.MaxValue).ToString()), Lastname = "Закас", Firstname = "Николас"};
		public static Author Simpson = new Author() { Id = Rnd.Next(int.MaxValue), Version = Encoding.ASCII.GetBytes(Rnd.Next(int.MaxValue).ToString()), Lastname = "Симпсон", Firstname = "Кайл"};
		public static Author Rihter = new Author() { Id = Rnd.Next(int.MaxValue), Version = Encoding.ASCII.GetBytes(Rnd.Next(int.MaxValue).ToString()), Lastname = "Рихтер", Firstname = "Джеффри" };
		public static Author Shildt = new Author() { Id = Rnd.Next(int.MaxValue), Version = Encoding.ASCII.GetBytes(Rnd.Next(int.MaxValue).ToString()), Lastname = "Шилдт", Firstname = "Герберт"};
		public static Author Troelsen = new Author() { Id = Rnd.Next(int.MaxValue), Version = Encoding.ASCII.GetBytes(Rnd.Next(int.MaxValue).ToString()), Lastname = "Троелсен", Firstname = "Эндрю"};
		public static Author Jepkins = new Author() { Id = Rnd.Next(int.MaxValue), Version = Encoding.ASCII.GetBytes(Rnd.Next(int.MaxValue).ToString()), Lastname = "Джепикс", Firstname = "Филипп"};
		public static Author Devis = new Author() { Id = Rnd.Next(int.MaxValue), Version = Encoding.ASCII.GetBytes(Rnd.Next(int.MaxValue).ToString()), Lastname = "Дэвис", Firstname = "Алекс"};
	}
}