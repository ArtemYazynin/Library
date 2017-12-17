using System.Linq;
using System.Threading.Tasks;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions.Subscriber;
using NUnit.Framework;


namespace Library.Tests.Services
{
	[TestFixture]
	internal sealed class SubscribersServiceTest: ServiceTestsBase
	{
		#region GetAll

		[Test]
		public async Task GetAll_ShouldReturnValidCount()
		{
			var subscribers = await SubscribersService.GetAll();
			Assert.That(subscribers.Count(), Is.EqualTo(Subscribers.Count));
			foreach (var subscriber in Subscribers)
			{
				Assert.That(subscriber.Lastname, Is.Not.Null.And.Not.Empty);
				Assert.That(subscriber.Firstname, Is.Not.Null.And.Not.Empty);
			}
		}

		#endregion


		#region GetById

		[Test]
		public async Task GetById_ShouldFindSubscriber()
		{
			var subscriber = await SubscribersService.GetById(DefaultData.Subscribers.Maslov.Id);

			Assert.That(subscriber, Is.Not.Null);
			Assert.That(subscriber.Id, Is.EqualTo(DefaultData.Subscribers.Maslov.Id));
		}


		[Test]
		public async Task GetById_ShoulReturnNull()
		{
			var subscriber = await SubscribersService.GetById(int.MaxValue);
			Assert.That(subscriber, Is.Null);
		}

		#endregion


		#region Create

		[Test]
		public async Task Create_ShouldCreated()
		{
			var oldCount = Subscribers.Count;
			var subscriber = new SubscriberDto()
			{
				Id = Random.Next(int.MaxValue),
				Lastname = "Novoselov",
				Firstname = "Oleg",
				Middlename = "Middlename'ovich"
			};
			await SubscribersService.Create(subscriber);

			Assert.That(Subscribers.Count, Is.EqualTo(oldCount+1));
			var created = Subscribers.Single(x => x.Id == subscriber.Id);
			Assert.That(created.Lastname, Is.EqualTo(subscriber.Lastname));
			Assert.That(created.Firstname, Is.EqualTo(subscriber.Firstname));
			Assert.That(created.Middlename, Is.EqualTo(subscriber.Middlename));
		}

		[Test]
		public void Create_ExistsSubscriber_ShouldThrownSubscriberDublicateException()
		{
			var dto = new SubscriberDto()
			{
				Lastname = DefaultData.Subscribers.Petrov.Lastname,
				Firstname = DefaultData.Subscribers.Petrov.Firstname,
				Middlename = DefaultData.Subscribers.Petrov.Middlename
			};

			Assert.Throws<SubscriberDublicateException>(async () => await SubscribersService.Create(dto));
		}

		[Test]
		public void Create_IncorrectSubscriber_ShouldThrownSubscriberIncorrectException()
		{
			var dto = new SubscriberDto();
			Assert.Throws<SubscriberIncorrectException>(async () => await SubscribersService.Create(dto));
		}

		#endregion


		#region Update

		[Test]
		public async Task Update_ShouldUpdated()
		{
			var dto = new SubscriberDto()
			{
				Id = DefaultData.Subscribers.Sidorov.Id,
				Lastname = "updateSubscriberTest_Lastname",
				Firstname = "updateSubscriberTest_Firstname",
				Middlename = "updateSubscriberTest_Middlename"
			};
			var returnVal = await SubscribersService.Update(dto.Id, dto);

			#region Assert return value

			Assert.That(returnVal.Lastname, Is.EqualTo(dto.Lastname));
			Assert.That(returnVal.Firstname, Is.EqualTo(dto.Firstname));
			Assert.That(returnVal.Middlename, Is.EqualTo(dto.Middlename));

			#endregion

			var updatedSubscriber = Subscribers.Single(x => x.Id == dto.Id);
			Assert.That(updatedSubscriber.Lastname, Is.EqualTo(dto.Lastname));
			Assert.That(updatedSubscriber.Firstname, Is.EqualTo(dto.Firstname));
			Assert.That(updatedSubscriber.Middlename, Is.EqualTo(dto.Middlename));
		}

		[Test]
		public void Update_ExistsSubscriber_ShouldThrownSubscriberDublicateException()
		{
			var dto = new SubscriberDto()
			{
				Id = DefaultData.Subscribers.Ivanov.Id,
				Lastname = DefaultData.Subscribers.Petrov.Lastname,
				Firstname = DefaultData.Subscribers.Petrov.Firstname,
				Middlename = DefaultData.Subscribers.Petrov.Middlename
			};

			Assert.Throws<SubscriberDublicateException>(async () => await SubscribersService.Update(dto.Id, dto));
		}

		[Test]
		public void Update_IncorrectSubscriber_ShouldThrownSubscriberIncorrectException()
		{
			var dto = new SubscriberDto() {Id = DefaultData.Subscribers.Ivanov.Id};
			Assert.Throws<SubscriberIncorrectException>(async () => await SubscribersService.Update(dto.Id, dto));
		}

		[Test]
		public void Update_IncorrectSubscriber_ShouldThrownSubscriberHasIncorrectIdException()
		{
			var dto = new SubscriberDto();
			Assert.Throws<SubscriberHasIncorrectIdException>(async () => await SubscribersService.Update(dto.Id, dto));
		}

		#endregion


		#region Delete

		[Test]
		public async Task Delete_ShouldDeleted()
		{
			var oldCount = Subscribers.Count;

			var subscriber = Subscribers.First();

			var returnVal = await SubscribersService.Delete(subscriber.Id);

			#region Assert returnVal

			Assert.That(returnVal.Lastname, Is.EqualTo(subscriber.Lastname));
			Assert.That(returnVal.Firstname, Is.EqualTo(subscriber.Firstname));
			Assert.That(returnVal.Middlename, Is.EqualTo(subscriber.Middlename));

			#endregion

			Assert.That(Subscribers.Count, Is.EqualTo(oldCount-1));
			Assert.That(Subscribers.SingleOrDefault(x=>x.Id == subscriber.Id), Is.Null);

		}

		[Test]
		public void Delete_ShouldThrownSubscriberHasActiveRentsException()
		{
			var subscriber = Subscribers.First();
			if (!subscriber.Rents.Any())
			{
				subscriber.Rents.Add(new Rent() {IsActive = true});
			}
			Assert.Throws<SubscriberHasActiveRentsException>(async () => await SubscribersService.Delete(subscriber.Id));
		}

		[Test]
		public async Task Delete_HasNotActiveRents_ShouldDelete()
		{
			var oldCount = Subscribers.Count;
			var subscriber = Subscribers.First();
			if (!subscriber.Rents.Any())
			{
				subscriber.Rents.Add(new Rent() { IsActive = false });
			}
			var returnVal = await SubscribersService.Delete(subscriber.Id);
			#region Assert returnVal

			Assert.That(returnVal.Lastname, Is.EqualTo(subscriber.Lastname));
			Assert.That(returnVal.Firstname, Is.EqualTo(subscriber.Firstname));
			Assert.That(returnVal.Middlename, Is.EqualTo(subscriber.Middlename));
			Assert.That(returnVal.IsDeleted, Is.True);
			#endregion

			Assert.That(Subscribers.Count, Is.EqualTo(oldCount));

			var deletedSubscriber = Subscribers.SingleOrDefault(x => x.Id == subscriber.Id);
			Assert.That(deletedSubscriber, Is.Not.Null);
			Assert.That(deletedSubscriber.IsDeleted, Is.True);
		}

		#endregion
	}
}