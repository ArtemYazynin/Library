using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions.Rent;
using NUnit.Framework;

namespace Library.Tests.Services
{
	[TestFixture]
	internal sealed class RentsServiceTest: ServiceTestsBase
	{
		#region GetAll

		[Test]
		public async Task GetAll_ShouldReturnValidCount()
		{
			var rents = await RentsService.GetAll();
			Assert.That(rents.Count(), Is.EqualTo(Rents.Count));
			foreach (var rent in Rents)
			{
				Assert.That(rent.Count, Is.Not.EqualTo(0));
				Assert.That(rent.Subscriber, Is.Not.Null);
				Assert.That(rent.Book, Is.Not.Null);
			}
		}

		#endregion


		#region GetById

		[Test]
		public async Task GetById_ShouldFindRent()
		{
			var rent = await RentsService.GetById(DefaultData.Rents.RentIvanov1.Id);

			Assert.That(rent, Is.Not.Null);
			Assert.That(rent.Id, Is.EqualTo(DefaultData.Rents.RentIvanov1.Id));
		}


		[Test]
		public async Task GetById_ShoulReturnNull()
		{
			var rent = await RentsService.GetById(int.MaxValue);
			Assert.That(rent, Is.Null);
		}

		#endregion


		#region Create

		[Test]
		public async Task Create_ShouldCreated()
		{
			var oldCount = Rents.Count;
			var rent = new RentDto()
			{
				Id = Random.Next(int.MaxValue),
				Count = 5,
				IsActive = true,
				Book = Mapper.Map<BookDto>(DefaultData.Books.ClrVia),
				Subscriber = Mapper.Map<SubscriberDto>(DefaultData.Subscribers.Maslov),
				Date = DateTime.Now
			};
			await RentsService.Create(rent);

			Assert.That(Rents.Count, Is.EqualTo(oldCount + 1));
			var created = Rents.SingleOrDefault(x => x.Id == rent.Id);
			Assert.That(created, Is.Not.Null);
			Assert.That(created.Count, Is.EqualTo(rent.Count));
			Assert.That(created.IsActive, Is.EqualTo(rent.IsActive));
			Assert.That(created.Date, Is.EqualTo(created.Date));

			Assert.That(created.Subscriber.ToString(), Is.EqualTo(rent.Subscriber.ToString()));
			Assert.That(created.Book.Id, Is.EqualTo(rent.Book.Id));
		}

		[Test]
		public void Create_ZeroCount_ShouldThrowRentNotHasZeroCountException()
		{
			var rent = new RentDto()
			{
				Id = Random.Next(int.MaxValue),
				Count = 0,
				IsActive = true,
				Book = Mapper.Map<BookDto>(DefaultData.Books.ClrVia),
				Subscriber = Mapper.Map<SubscriberDto>(DefaultData.Subscribers.Maslov),
				Date = DateTime.Now
			};

			Assert.Throws<RentNotHasZeroCountException>(async () => await RentsService.Create(rent));
		}

		[Test]
		public void Create_BookNull_ShouldThrowRentNotHasBookOrSubscriberException()
		{
			var rent = new RentDto()
			{
				Id = Random.Next(int.MaxValue),
				Count = 55,
				IsActive = true,
				Subscriber = Mapper.Map<SubscriberDto>(DefaultData.Subscribers.Maslov),
				Date = DateTime.Now
			};

			Assert.Throws<RentNotHasBookOrSubscriberException>(async () => await RentsService.Create(rent));
		}

		[Test]
		public void Create_SubscriberNull_ShouldThrowRentNotHasBookOrSubscriberException()
		{
			var rent = new RentDto()
			{
				Id = Random.Next(int.MaxValue),
				Count = 55,
				IsActive = true,
				Book = Mapper.Map<BookDto>(DefaultData.Books.ClrVia),
				Date = DateTime.Now
			};

			Assert.Throws<RentNotHasBookOrSubscriberException>(async () => await RentsService.Create(rent));
		}

		[Test]
		public void Create_CountMoreCountOfBooks_ShouldThrownRentCountMoreCountOfBookException()
		{
			var rent = new RentDto()
			{
				Id = Random.Next(int.MaxValue),
				Count = DefaultData.Books.ClrVia.Count+1,
				IsActive = true,
				Book = Mapper.Map<BookDto>(DefaultData.Books.ClrVia),
				Subscriber = Mapper.Map<SubscriberDto>(DefaultData.Subscribers.Maslov),
				Date = DateTime.Now
			};
			Assert.Throws<RentCountMoreCountOfBookException>(async () => await RentsService.Create(rent));
		}

		#endregion


	}
}