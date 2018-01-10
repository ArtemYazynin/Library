using System;
using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Rents
	{
		private static readonly Random Rnd = new Random();

		static Rents()
		{
			RentIvanov1 = new Rent(Books.ClrVia, Subscribers.Ivanov, 1, true)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 17)
			};
			RentIvanov2 = new Rent(Books.MyEvernoteNotes, Subscribers.Ivanov,1,true)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 17)
			};

			RentIvanov3 = new Rent(Books.CSharp6AndNetPlatform, Subscribers.Ivanov,2,true)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 20)
			};

			RentPetrov = new Rent(Books.JsPocketGuide, Subscribers.Petrov,2,true)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 18)
			};

			RentSidorov = new Rent(Books.JsOptimizingPerfomance, Subscribers.Sidorov,4)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 16)
			};

			RentMaslov = new Rent(Books.AsyncProgrammingCSharp5, Subscribers.Maslov,1)
			{
				Id = Rnd.Next(int.MaxValue),
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