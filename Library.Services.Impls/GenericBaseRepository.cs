using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.ObjectModel.Models;

namespace Library.Services.Impls
{
	public class GenericBaseRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
	{
		protected LibraryContext Context;
		protected IDbSet<TEntity> DbSet;

		protected GenericBaseRepository(LibraryContext context)
		{
			Context = context;
			DbSet = context.Set<TEntity>();
		}

		public virtual async Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters = null, 
							Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
							string includeProperties = "")
		{
			IQueryable<TEntity> query = DbSet;

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
			ApplyIncludeProperties(includeProperties, ref query);
			if (orderBy == null)
			{
				return await query.ToListAsync();
			}
			return await orderBy.Invoke(query).ToListAsync();
		}
		private void ApplyIncludeProperties(string includeProperties, ref IQueryable<TEntity> query)
		{
			foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}
		}


		public virtual async Task<TEntity> Get(long id, string includeProperties = "")
		{
			IQueryable<TEntity> query = DbSet;
			ApplyIncludeProperties(includeProperties, ref query);

			return await query.SingleAsync(x => x.Id == id);
		}

		public virtual bool Create(TEntity entity)
		{
			if (entity == null) return false;
			DbSet.Add(entity);
			return true;
		}

		public virtual bool Delete(long id)
		{
			var entity = DbSet.Find(id);
			if (entity == null) return false;

			DbSet.Remove(entity);
			return true;
		}

		public virtual bool Delete(TEntity entity)
		{
			if (entity == null) return false;

			if (Context.Entry(entity).State == EntityState.Detached)
			{
				DbSet.Attach(entity);
			}
			DbSet.Remove(entity);
			return true;
		}

		public virtual bool Update(TEntity entity)
		{
			DbSet.Attach(entity);
			Context.Entry(entity).State = EntityState.Modified;
			return true;
		}
	}
}