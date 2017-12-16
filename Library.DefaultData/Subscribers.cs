using System;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Subscribers
	{
		private static readonly Random Rnd = new Random();
		public static Subscriber Ivanov = new Subscriber()
		{
			Id = Rnd.Next(int.MaxValue),
			Lastname = "Иванов",
			Firstname = "Иван",
			Middlename = "Иванович"
		};

		public static Subscriber Petrov = new Subscriber()
		{
			Id = Rnd.Next(int.MaxValue),
			Lastname = "Петров",
			Firstname = "Петр",
			Middlename = "Петрович"
		};

		public static Subscriber Sidorov = new Subscriber()
		{
			Id = Rnd.Next(int.MaxValue),
			Lastname = "Сидоров",
			Firstname = "Матвей",
			Middlename = "Матвеевич"
		};

		public static Subscriber Maslov = new Subscriber()
		{
			Id = Rnd.Next(int.MaxValue),
			Lastname = "Маслов",
			Firstname = "Андрей",
			Middlename = "Евгениевич"
		};
	}
}