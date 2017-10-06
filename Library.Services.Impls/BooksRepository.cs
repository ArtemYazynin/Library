using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Library.ObjectModel.Models;

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


		public async override Task<bool> Update(Book entity)
		{
			var dbEntity = await Get(entity.Id, $"{nameof(Edition)}, {nameof(Publisher)}, {nameof(Book.Authors)}, {nameof(Book.Genres)}");
			Context.Entry(dbEntity).CurrentValues.SetValues(entity);
			if (dbEntity.EditionId != entity.EditionId)
			{
				Context.Editions.Attach(entity.Edition);
				
				dbEntity.Edition = entity.Edition;
			}
			if (dbEntity.PublisherId != entity.PublisherId)
			{
				Context.Publishers.Attach(entity.Publisher);
				dbEntity.Publisher = entity.Publisher;
			}


			return true;
		}
	}
}