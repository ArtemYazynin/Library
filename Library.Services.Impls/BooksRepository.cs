using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	public class BooksRepository: IGenericRepository<Book>
	{
		private readonly LibraryContext _context;
		private readonly IDbSet<Book> _dbSet;

		public BooksRepository(LibraryContext context)
		{
			_context = context;
			_dbSet = context.Set<Book>();
		}

		public async Task<IEnumerable<Book>> GetAllAsync(IEnumerable<Expression<Func<Book, bool>>> filters = null, 
														 Func<IQueryable<Book>, IOrderedQueryable<Book>> orderBy = null, 
														 string includeProperties = "")
		{
			IQueryable<Book> query = _dbSet;

			if (filters != null)
			{
				foreach (Expression<Func<Book, bool>> expression in filters)
				{
					query = query.Where(expression);
				}
			}

			foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}
			if (orderBy == null)
			{
				return await query.ToListAsync();
			}
			return await orderBy.Invoke(query).ToListAsync();
		}

		public async Task<Book> Get(long id)
		{
			var dbSet = _dbSet as DbSet<Book>;
			if (dbSet == null)
			{
				return _dbSet.Find(id);
			}
			return await dbSet.FindAsync(id);
		}

		public bool Create(Book entity)
		{
			if (entity == null) return false;

			if (entity.EditionId != default(long))
				_context.Editions.Attach(entity.Edition);
			if(entity.PublisherId != default(long))
				_context.Publishers.Attach(entity.Publisher);
			if (entity.Authors.Any())
			{
				foreach (Author author in entity.Authors)
				{
					if (author.Id != default(long))
					{
						_context.Authors.Attach(author);
					}
					
				}
			}
			if (entity.Genres.Any())
			{
				foreach (Genre genre in entity.Genres)
				{
					if (genre.Id != default(long))
					{
						_context.Genres.Attach(genre);
					}
				}
			}

			_dbSet.Add(entity);
			return true;
		}

		public bool Delete(long id)
		{
			var entity = _dbSet.Find(id);
			if (entity == null) return false;

			_dbSet.Remove(entity);
			return true;
		}

		public bool Delete(Book entity)
		{
			if (entity == null) return false;

			if (_context.Entry(entity).State == EntityState.Detached)
			{
				_dbSet.Attach(entity);
			}
			_dbSet.Remove(entity);
			return true;
		}

		public bool Update(Book entity)
		{
			if (entity == null) return false;

			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			return true;
		}
	}
}