using System;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Subscribers
	{
		private static readonly Random Rnd = new Random();
		public static Subscriber Ivanov = new Subscriber("Иванов", "Иван")
		{
			Id = Rnd.Next(int.MaxValue),
			Middlename = "Иванович"
		};

		public static Subscriber Petrov = new Subscriber("Петров", "Петр")
		{
			Id = Rnd.Next(int.MaxValue),
			Middlename = "Петрович"
		};

		public static Subscriber Sidorov = new Subscriber("Сидоров", "Матвей")
		{
			Id = Rnd.Next(int.MaxValue),
			Middlename = "Матвеевич"
		};

		public static Subscriber Maslov = new Subscriber("Маслов", "Андрей")
		{
			Id = Rnd.Next(int.MaxValue),
			Middlename = "Евгениевич"
		};
	}
}