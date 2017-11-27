using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.Services.DTO;
using Library.Services.Impls.Exceptions;
using NUnit.Framework;

namespace Library.Tests.Services.Genres
{
	sealed class GenresServiceTest: ServiceTestsBase
	{
		#region Get methods

		[Test]
		public async Task GetAll_ShouldReturnValidCount()
		{
			var genres = await GenresService.GetAll();

			Assert.That(genres.Count(), Is.EqualTo(Genres.Count));
		}

		[Test]
		public async Task GetTree_ShouldReturnRootNodeAndChildren()
		{
			var genres = await GenresService.GetTree();
			foreach (var genre in genres)
			{
				Assert.That(genre.Parent, Is.Null);
			}
		}

		#endregion


		#region Delete

		[Test]
		public async Task Delete_RootNodes_ShouldDeleteAllGenres()
		{
			var rootGenres = Genres.Where(x => x.Parent == null).ToList();

			for (int i = 0, len = rootGenres.Count(); i < len; i++)
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
			Assert.That(genres.Where(x => x.Id == genre.Id), Is.Empty);
			Assert.That(genres, Is.Empty);
		}

		[Test]
		public async Task Delete_NodeNoneRecursibelly_ShouldDeleteOnlyTargetNode()
		{
			var genre = Genres.Single(x => x.Id == DefaultData.Genres.Programming.Id);

			await GenresService.Delete(genre.Id, true);
		}

		#endregion


		#region Create

		[Test]
		public void Create_IncorrectName_ShouldThrownGenreIncorrectException()
		{
			GenreDto dto = new GenreDto();
			Assert.Throws<GenreIncorrectException>(async () => await GenresService.Create(dto));
		}

		[Test]
		public void Create_ExistsGenre_ShouldThrownGenreDublicateException()
		{
			var dto = new GenreDto(){Name = DefaultData.Genres.CSharp.Name};
			Assert.Throws<GenreDublicateException>(async ()=> await GenresService.Create(dto));
		}

		[Test]
		public async Task Create_RootGenre_ShouldValid()
		{
			var oldCount = Genres.Count;
			var dto = new GenreDto(){ Name = nameof(Create_RootGenre_ShouldValid) };
			await GenresService.Create(dto);

			Assert.That(Genres.Count, Is.EqualTo(oldCount+1));
			var createdGenre = Genres.SingleOrDefault(x => string.Equals(x.Name, nameof(Create_RootGenre_ShouldValid), StringComparison.CurrentCultureIgnoreCase));
			Assert.That(createdGenre, Is.Not.Null);
			Assert.That(createdGenre.Parent, Is.Null);
			Assert.That(createdGenre.Children, Is.Empty);
		}

		[Test]
		public async Task Create_ChildGenre_ShouldValid()
		{
			var oldCount = Genres.Count;
			var dto = new GenreDto() { Name = nameof(Create_ChildGenre_ShouldValid), Parent = Mapper.Map<GenreDto>(DefaultData.Genres.CSharp) };
			await GenresService.Create(dto);

			Assert.That(Genres.Count, Is.EqualTo(oldCount + 1));
			var createdGenre = Genres.SingleOrDefault(x => string.Equals(x.Name, nameof(Create_ChildGenre_ShouldValid), StringComparison.CurrentCultureIgnoreCase));
			Assert.That(createdGenre, Is.Not.Null);
			Assert.That(createdGenre.Parent.Id, Is.EqualTo(DefaultData.Genres.CSharp.Id));
			Assert.That(createdGenre.Children, Is.Empty);
		}

		#endregion


	}
}