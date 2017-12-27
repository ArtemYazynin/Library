using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	public class GenresRepository: GenericBaseRepository<Genre>, IGenresRepository
	{
		public GenresRepository() : base(null)
		{
			
		}
		public GenresRepository(LibraryContext context) : base(context)
		{
		}

		public async Task<IEnumerable<Genre>> GetTree(IList<Expression<Func<Genre, bool>>> filters)
		{
			var includeProperties = $"{nameof(Genre.Children)}, {nameof(Genre.Parent)}";
			var genres = await GetAllAsync(filters, null, includeProperties);
			await RecurcivelyLoadChildren(genres);
			return genres;
		}

		private async Task RecurcivelyLoadChildren(IEnumerable<Genre> genres)
		{
			foreach (var genre in genres)
			{
				Context.Genres.Attach(genre);
				var entry = Context.Entry(genre);
				
				if (!entry.Collection(nameof(genre.Children)).IsLoaded)
				{
					await entry.Collection(nameof(Genre.Children)).LoadAsync();
				}
				if (genre.Children.Any())
				{
					await RecurcivelyLoadChildren(genre.Children);
				}
			}
		}

		public override async Task<Genre> Get(long id, string includeProperties = "")
		{
			var genre = await base.Get(id, includeProperties);
			await RecurcivelyLoadChildren(new List<Genre>() { genre });
			return genre;
		}

		public override bool Create(Genre entity)
		{
			if (entity.Parent != null)
			{
				if (Context.Entry(entity.Parent).State == EntityState.Detached)
				{
					Context.Genres.Attach(entity.Parent);
				}
			}

			return base.Create(entity);
		}
	}
}