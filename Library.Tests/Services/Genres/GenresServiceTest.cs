using System.Linq;
using System.Threading.Tasks;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions;
using NUnit.Framework;

namespace Library.Tests.Services.Genres
{
	[TestFixture]
	sealed class GenresServiceTest: ServiceTestsBase
	{
		#region Update

		[Test]
		public async Task Update_ShouldUpdate([Values("Very updated Name!!!")]string name)
		{
			var updatedGenre = Genres.Single(x=>x.Id == DefaultData.Genres.Programming.Id);
			var  dto = new GenreDto()
			{
				Id = updatedGenre.Id,
				Version = updatedGenre.Version,
				Name = name
			};
			await GenresService.Update(updatedGenre.Id, dto);

			var gen = Genres.Single(x => x.Id == updatedGenre.Id);
			Assert.That(gen.Name, Is.EqualTo(dto.Name));
		}

		[Test]
		public void Update_IncorrectName_ShouldThrownGenreIncorrectException()
		{
			Assert.Throws<GenreIncorrectException>(async () => await Update_ShouldUpdate(string.Empty));
		}

		[Test]
		public void Update_ExistingAuthor_ShouldThrownDublicateException()
		{
			Assert.Throws<GenreDublicateException>(async () => await Update_ShouldUpdate(DefaultData.Genres.CSharp.Name));
		}

		#endregion

		[Test]
		public async Task GetAll_ShouldReturnValidCount()
		{
			var rootGenres = Genres.Where(x => x.Parent == null);
			var genres = await GenresService.GetAll();

			Assert.That(genres.Count(), Is.EqualTo(rootGenres.Count()));
		}

		#region Delete

		[Test]
		public void Delete_RecursibellyRootNodes_ShouldDeleteAllGenres()
		{
			var rootGenres = Genres.Where(x=>x.Parent == null).ToList();

			for (int i = 0, len=rootGenres.Count(); i < len; i++)
			{
				GenresService.Delete(rootGenres[i].Id, true);
			}

			Assert.That(Genres, Is.Empty);
		}

		#endregion
	}
}