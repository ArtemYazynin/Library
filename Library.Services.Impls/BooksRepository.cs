using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Library.ObjectModel.Models;
using Library.Services.Impls.Comparers;

namespace Library.Services.Impls
{
	public class BooksRepository: GenericBaseRepository<Book>
	{
		public BooksRepository(LibraryContext context) : base(context)
		{
		}

		public override bool Create(Book entity)
		{
			if (entity == null) return false;

			if (entity.EditionId != default(long))
				Context.Editions.Attach(entity.Edition);
			if (entity.PublisherId != default(long))
				Context.Publishers.Attach(entity.Publisher);
			if (entity.Authors.Any())
			{
				foreach (Author author in entity.Authors)
				{
					if (author.Id != default(long))
					{
						Context.Authors.Attach(author);
					}

				}
			}
			if (entity.Genres.Any())
			{
				foreach (Genre genre in entity.Genres)
				{
					if (genre.Id != default(long))
					{
						Context.Genres.Attach(genre);
					}
				}
			}

			DbSet.Add(entity);
			return true;
		}


		public override bool Update(Book entity)
		{
			if (Context.Entry(entity).State == EntityState.Modified)
			{
				Context.Books.Attach(entity);
			}
			Context.Editions.Attach(entity.Edition);
			Context.Publishers.Attach(entity.Publisher);

			foreach (var author in entity.Authors)
			{
				Context.Authors.Attach(author);
			}
			foreach (var genre in entity.Genres)
			{
				Context.Genres.Attach(genre);
			}
			return true;
		}
	}
}