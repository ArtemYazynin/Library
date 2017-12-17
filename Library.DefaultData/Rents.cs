using System;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Rents
	{
		private static readonly Random Rnd = new Random();

		static Rents()
		{
			RentIvanov1 = new Rent()
			{
				Id = Rnd.Next(int.MaxValue),
				Subscriber = Subscribers.Ivanov,
				Book = Books.ClrVia,
				Count = 1,
				IsActive = true,
				Date = new DateTime(2017, 12, 17)
			};
			RentIvanov2 = new Rent()
			{
				Id = Rnd.Next(int.MaxValue),
				Subscriber = Subscribers.Ivanov,
				Book = Books.MyEvernoteNotes,
				Count = 1,
				IsActive = true,
				Date = new DateTime(2017, 12, 17)
			};

			RentIvanov3 = new Rent()
			{
				Id = Rnd.Next(int.MaxValue),
				Subscriber = Subscribers.Ivanov,
				Book = Books.CSharp6AndNetPlatform,
				Count = 2,
				IsActive = true,
				Date = new DateTime(2017, 12, 20)
			};

			RentPetrov = new Rent()
			{
				Id = Rnd.Next(int.MaxValue),
				Subscriber = Subscribers.Petrov,
				Book = Books.JsPocketGuide,
				Count = 2,
				IsActive = true,
				Date = new DateTime(2017, 12, 18)
			};

			RentSidorov = new Rent()
			{
				Id = Rnd.Next(int.MaxValue),
				Subscriber = Subscribers.Sidorov,
				Book = Books.JsOptimizingPerfomance,
				Count = 4,
				IsActive = false,
				Date = new DateTime(2017, 12, 16)
			};

			RentMaslov = new Rent()
			{
				Id = Rnd.Next(int.MaxValue),
				Subscriber = Subscribers.Maslov,
				Book = Books.AsyncProgrammingCSharp5,
				Count = 1,
				IsActive = false,
				Date = new DateTime(2017, 12, 15)
			};
		}

		public static Rent RentIvanov1 { get; }
		public static Rent RentIvanov2 { get; }
		public static Rent RentIvanov3 { get; }

		public static Rent RentPetrov { get; }
		public static Rent RentSidorov { get; }
		public static Rent RentMaslov { get; }
	}
}