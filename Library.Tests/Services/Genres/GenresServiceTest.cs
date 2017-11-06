using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Library.Tests.Services.Genres
{
	[TestFixture]
	sealed class GenresServiceTest: ServiceTestsBase
	{
		[Test]
		public async Task GetAll_ShouldReturnValidCount()
		{
			var rootGenres = Genres.Where(x => x.Parent == null);
			var genres = await GenresService.GetAll();

			Assert.That(genres.Count(), Is.EqualTo(rootGenres.Count()));
		}

		[Test]
		public void Delete_RootNodes_ShouldDeleteAllGenres()
		{
			var rootGenres = Genres.Where(x=>x.Parent == null).ToList();

			for (int i = 0, len=rootGenres.Count(); i < len; i++)
			{
				GenresService.Delete(rootGenres[i].Id, true);
			}

			Assert.That(Genres, Is.Empty);
		}
	}
}