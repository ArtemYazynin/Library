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
				Date = new DateTime(2017, 12, 11)
			};
			RentIvanov2 = new Rent(Books.MyEvernoteNotes, Subscribers.Ivanov,1,true)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 12)
			};

			RentIvanov3 = new Rent(Books.CSharp6AndNetPlatform, Subscribers.Ivanov,2,true)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 14)
			};

			RentPetrov = new Rent(Books.JsPocketGuide, Subscribers.Petrov,2,true)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 19)
			};
			//////////////////////////////////////////////////////////////////////////////////////
			RentSidorov = new Rent(Books.JsOptimizingPerfomance, Subscribers.Sidorov,40)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 16)
			};
			RentSidorov2 = new Rent(Books.WithoutAuthorsBook, Subscribers.Sidorov, 1)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 16)
			};
			RentSidorov3 = new Rent(Books.Es6AndNotOnly, Subscribers.Sidorov, 40)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 16)
			};
			RentSidorov4 = new Rent(Books.JsForProfessionals, Subscribers.Sidorov, 10)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 16)
			};
			RentSidorov5 = new Rent(Books.CSharp6AndNetPlatform, Subscribers.Sidorov, 12)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 16)
			};
			//////////////////////////////////////////////////////////////////////////////////////
			RentMaslov = new Rent(Books.AsyncProgrammingCSharp5, Subscribers.Maslov,1)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 15)
			};
			RentMaslov2 = new Rent(Books.AsyncProgrammingCSharp5, Subscribers.Maslov, 2)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 16)
			};
			RentMaslov3 = new Rent(Books.ClrVia, Subscribers.Maslov, 5)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 17)
			};
			RentMaslov4 = new Rent(Books.ClrVia, Subscribers.Maslov, 5)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 18)
			};
			RentMaslov5 = new Rent(Books.ClrVia, Subscribers.Maslov, 5)
			{
				Id = Rnd.Next(int.MaxValue),
				Date = new DateTime(2017, 12, 18)
			};
			//////////////////////////////////////////////////////////////////////////////////////////
		}

		public static Rent RentIvanov1 { get; }
		public static Rent RentIvanov2 { get; }
		public static Rent RentIvanov3 { get; }

		public static Rent RentPetrov { get; }

		#region Sidorov(5)

		public static Rent RentSidorov { get; }
		public static Rent RentSidorov2 { get; }
		public static Rent RentSidorov3 { get; }
		public static Rent RentSidorov4 { get; }
		public static Rent RentSidorov5 { get; }

		#endregion

		#region Maslov(5)

		public static Rent RentMaslov { get; }
		public static Rent RentMaslov2 { get; }
		public static Rent RentMaslov3 { get; }
		public static Rent RentMaslov4 { get; }
		public static Rent RentMaslov5 { get; }

		#endregion


	}
}