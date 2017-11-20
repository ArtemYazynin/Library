using System.Linq;
using System.Threading.Tasks;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions;
using NUnit.Framework;

namespace Library.Tests.Services.Authors
{
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
		public async Task GetById_ShoulReturnNull()
		{
			var author = await AuthorsService.Get(int.MaxValue);
			Assert.That(author, Is.Null);
		}

		#endregion

		#region Create

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
			Assert.That(Authors.Count, Is.EqualTo(oldCount + 1));
			Assert.That(await AuthorsService.Get(authorDto.Id), Is.Not.Null);
		}

		[Test]
		public void Create_ExistsAuthor_ShouldThrownAuthorDublicateException()
		{
			var dto = new AuthorDto()
			{
				Lastname = DefaultData.Authors.Flenagan.Lastname,
				Firstname = DefaultData.Authors.Flenagan.Firstname,
				Middlename = DefaultData.Authors.Flenagan.Middlename,
			};

			Assert.Throws<AuthorDublicateException>(async () => await AuthorsService.Create(dto));
		}

		[Test]
		public void Create_InvalidDto_ShouldThrownAuthorIncorrectException()
		{
			var dto = new AuthorDto();
			Assert.Throws<AuthorIncorrectException>(async () => await AuthorsService.Create(dto));
		}

		#endregion

		#region Delete

		[Test]
		public async Task Delete_ShouldDeleted()
		{
			var countBefore = (await AuthorsService.GetAll()).Count();

			await AuthorsService.Delete(DefaultData.Authors.Rihter.Id);

			var countAfterDelete = (await AuthorsService.GetAll()).Count();
			var deletedAuthor = await AuthorsService.Get(DefaultData.Authors.Rihter.Id);
			Assert.That(countAfterDelete, Is.EqualTo(countBefore - 1));
			Assert.That(deletedAuthor, Is.Null);
		}

		[Test]
		public async Task Delete_NoneExistendAuthor_ShouldReturnNull()
		{
			var countBefore = (await AuthorsService.GetAll()).Count();

			await AuthorsService.Delete(int.MaxValue);

			var countAfter = (await AuthorsService.GetAll()).Count();
			Assert.That(countAfter, Is.EqualTo(countBefore));
		}

		#endregion

		#region Update

		[Test]
		public async Task Update_ShouldUpdate()
		{
			var dto = new AuthorDto()
			{
				Id = DefaultData.Authors.Rihter.Id,
				Version = DefaultData.Authors.Rihter.Version,
				Lastname = int.MinValue.ToString(),
				Firstname = int.MaxValue.ToString(),
				Middlename = default(int).ToString()
			};

			await AuthorsService.Update(DefaultData.Authors.Rihter.Id, dto);

			Assert.That(DefaultData.Authors.Rihter.Lastname, Is.EqualTo(int.MinValue.ToString()));
			Assert.That(DefaultData.Authors.Rihter.Firstname, Is.EqualTo(int.MaxValue.ToString()));
			Assert.That(DefaultData.Authors.Rihter.Middlename, Is.EqualTo(default(int).ToString()));
		}

		[Test]
		public void Update_ExistingAuthor_ShouldThrownDublicateException()
		{
			var dto = new AuthorDto()
			{
				Id = DefaultData.Authors.Devis.Id,
				Version = DefaultData.Authors.Devis.Version,
				Lastname = DefaultData.Authors.Ferguson.Lastname,
				Firstname = DefaultData.Authors.Ferguson.Firstname,
				Middlename = DefaultData.Authors.Ferguson.Middlename
			};

			Assert.Throws<AuthorDublicateException>(async () => await AuthorsService.Update(dto.Id, dto));
		}

		[Test]
		public void Update_InvalidAuthor_ShouldThrownAuthorIncorrectException()
		{
			var dto = new AuthorDto()
			{
				Id = DefaultData.Authors.Devis.Id
			};
			Assert.Throws<AuthorIncorrectException>(async () => await AuthorsService.Update(dto.Id, dto));

		}
		#endregion
	}
}
