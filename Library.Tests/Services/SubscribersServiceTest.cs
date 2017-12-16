using System.Linq;
using System.Threading.Tasks;
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
			var oldCount = Authors.Count;
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
	}
}