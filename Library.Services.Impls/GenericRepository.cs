using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
	{
		private readonly LibraryContext _context;
		private readonly IDbSet<TEntity> _dbSet;

		public GenericRepository(LibraryContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "")
		{
			IQueryable<TEntity> query = _dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			return orderBy?.Invoke(query).ToList() ?? query.ToList();
		}

		public TEntity Get(long id)
		{
			return _dbSet.Find(id);
		}

		public void Create(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public void Delete(long id)
		{
			var entity = _dbSet.Find(id);
			if (entity == null) return;

			_dbSet.Remove(entity);
		}

		public void Delete(TEntity entity)
		{
			if (_context.Entry(entity).State == EntityState.Detached)
			{
				_dbSet.Attach(entity);
			}
			_dbSet.Remove(entity);
		}

		public void Update(TEntity entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
	}
}