using System;
using System.Linq;
using System.Threading.Tasks;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions.Publisher;
using NUnit.Framework;

namespace Library.Tests.Services
{
	sealed class PublishersServiceTest : ServiceTestsBase
	{
		#region GetAll

		[Test]
		public async Task GetAll_ShouldReturnValid()
		{
			var publishers = await PublishersService.GetAll();

			Assert.That(publishers.Count(), Is.EqualTo(Publishers.Count));
		}

		#endregion


		#region Delete

		[Test]
		public void Delete_PublisherHasBooks_ShouldThrowPublisherHasBooksException()
		{
			var publisher = Publishers.First(x => x.Books.Any());
			Assert.Throws<PublisherHasBooksException>(async () =>
			{
				await PublishersService.Delete(publisher.Id);
			});
		}

		[Test]
		public async Task Delete_ShouldDeletePublisher()
		{
			var oldCount = Publishers.Count;
			var publisher = Publishers.First();
			publisher.Books.Clear();

			await PublishersService.Delete(publisher.Id);

			var newCount = Publishers.Count;

			Assert.That(newCount, Is.EqualTo(oldCount - 1));
			Assert.That(Publishers.SingleOrDefault(x => x.Id == publisher.Id), Is.Null);
		}

		#endregion


		#region Update

		[Test]
		public void Update_ExistingName_ShouldThrownPublisherDublicateException()
		{
			var existingPublisher = Publishers.First();
			PublisherDto dto = new PublisherDto()
			{
				Id = existingPublisher.Id,
				Name = existingPublisher.Name
			};

			Assert.Throws<PublisherDublicateException>(async () => await PublishersService.Update(dto.Id, dto));
		}

		[Test]
		public void Update_NameIncorrect_ShouldThrownPublisherIncorrectException()
		{
			var publisher = Publishers.First();
			PublisherDto dto = new PublisherDto();

			Assert.Throws<PublisherIncorrectException>(async () => await PublishersService.Update(publisher.Id, dto));
		}

		[Test]
		public async Task Update_ShouldUpdate()
		{
			var publisher = Publishers.First();
			PublisherDto dto = new PublisherDto()
			{
				Id = publisher.Id,
				Name = "very updated name"
			};
			await PublishersService.Update(publisher.Id, dto);

			Assert.That(Publishers.Single(x => x.Id == dto.Id).Name, Is.EqualTo(dto.Name));
		}

		#endregion


		#region Create

		[Test]
		public void Create_ExistingName_ShouldThrownPublisherDublicateException()
		{
			var oldCount = Publishers.Count;
			var publisher = Publishers.First();
			var dto = new PublisherDto()
			{
				Name = publisher.Name
			};


			Assert.Throws<PublisherDublicateException>(async ()=> await PublishersService.Create(dto));
			Assert.That(Publishers.Count, Is.EqualTo(oldCount));
		}

		[Test]
		public void Create_InvalidName_ShouldThrownPublisherIncorrectException()
		{
			var oldCount = Publishers.Count;
			var dto = new PublisherDto();
			Assert.Throws<PublisherIncorrectException>(async () => await PublishersService.Create(dto));
			Assert.That(Publishers.Count, Is.EqualTo(oldCount));
		}


		[Test]
		public async Task Create_ShouldCreate()
		{
			PublisherDto dto = new PublisherDto()
			{
				Name = "very created name"
			};
			await PublishersService.Create(dto);

			Assert.That(Publishers.Single(x => string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase)), 
						Is.Not.Empty);
		}
		#endregion
	}
}