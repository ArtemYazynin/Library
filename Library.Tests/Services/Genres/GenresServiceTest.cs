﻿using System.Linq;
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
		public async Task Delete_RootNodes_ShouldDeleteAllGenres()
		{
			var rootGenres = Genres.Where(x=>x.Parent == null).ToList();

			for (int i = 0, len=rootGenres.Count(); i < len; i++)
			{
				await GenresService.Delete(rootGenres[i].Id, true);
			}

			Assert.That(Genres, Is.Empty);
		}

		[Test]
		public async Task Delete_NodeWithNestedNodesRecurcivelly_ShouldDeleteNodeWithNestedNodes()
		{
			var genre = Genres.Single(x => x.Id == DefaultData.Genres.Programming.Id);

			await GenresService.Delete(genre.Id, true);

			var genres = Genres.Where(x => x.Parent?.Id == genre.Id);
			Assert.That(genres.Where(x=>x.Id == genre.Id), Is.Empty);
			Assert.That(genres, Is.Empty);
		}
	}
}