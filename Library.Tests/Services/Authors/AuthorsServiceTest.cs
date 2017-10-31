

using System;
using System.Linq;
using System.Threading.Tasks;
using Library.Services.DTO;
using NUnit.Framework;

namespace Library.Tests.Services.Authors
{
	[TestFixture]
	internal sealed class AuthorsServiceTest: ServiceTestsBase
	{
		[Test]
		public async Task GetAll_ShouldReturnValidCount()
		{
			var authors = await AuthorsService.GetAll();

			Assert.That(authors.Count(), Is.EqualTo(Authors.Count));
		}

		#region GetById

		[Test]
		public async Task GetById_ShouldFindAuthor()
		{
			var author = await AuthorsService.Get(DefaultData.Authors.Flenagan.Id);

			Assert.That(author, Is.Not.Null);
			Assert.That(author.Id, Is.EqualTo(DefaultData.Authors.Flenagan.Id));
		}

		[Test]
		public async Task GetById_ShoulThrowException()
		{
			var author = await AuthorsService.Get(int.MaxValue);
			Assert.That(author, Is.Null);
		}

		#endregion

		[Test]
		public async Task Create_ShouldCreated()
		{
			Assert.That((await AuthorsService.GetAll()).Count(), Is.EqualTo(Authors.Count));
			var oldCount = Authors.Count;
			var id = Random.Next(int.MaxValue);
			var authorDto = new AuthorDto()
			{
				Id = id,
				Lastname = "Rotenberg",
				Firstname = "Arkadii",
				Middlename = "Bar"
			};

			await AuthorsService.Create(authorDto);
			Assert.That(Authors.Count, Is.EqualTo(oldCount+1));
			Assert.That(await AuthorsService.Get(authorDto.Id), Is.Not.Null);
		}
	}
}
