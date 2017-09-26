using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
	{
		private readonly LibraryContext _context;
		private readonly DbSet<TEntity> _dbSet;

		public GenericRepository(LibraryContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public IEnumerable<TEntity> GetAll(IEnumerable<Expression<Func<TEntity, bool>>> filters = null, 
										   Func<IQueryable<TEntity>, 
										   IOrderedQueryable<TEntity>> orderBy = null, 
										   string includeProperties = "")
		{
			IQueryable<TEntity> query = _dbSet;

			if (filters != null)
			{
				foreach (Expression<Func<TEntity, bool>> expression in filters)
				{
					query = query.Where(expression);
				}
				
			}

			foreach (var includeProperty in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			return orderBy?.Invoke(query).ToList() ?? query.ToList();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
		{
			IQueryable<TEntity> query = _dbSet;

			if (filters != null)
			{
				foreach (Expression<Func<TEntity, bool>> expression in filters)
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

		public Task<TEntity> Get(long id)
		{
			return _dbSet.FindAsync(id);
		}

		public  bool Create(TEntity entity)
		{
			if (entity == null) return false;
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

		public bool Delete(TEntity entity)
		{
			if (entity == null) return false;

			if (_context.Entry(entity).State == EntityState.Detached)
			{
				_dbSet.Attach(entity);
			}
			_dbSet.Remove(entity);
			return true;
		}

		public bool Update(TEntity entity)
		{
			if (entity == null) return false;

			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			return true;
		}
	}
}