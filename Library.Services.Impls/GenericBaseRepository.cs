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
							string includeProperties = "",
							int skip = 0,
							int? take = null)
		{
			IQueryable<TEntity> query = DbSet.AsNoTracking();

			//if (skip != 0) query = query.Skip(skip);
			//if (take.HasValue) query = query.Take(take.Value);

			#region filters

			if (filters != null)
			{
				foreach (Expression<Func<TEntity, bool>> expression in filters)
				{
					query = query.Where(expression);
				}
			}

			#endregion


			#region includes

			foreach (var includeProperty in includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}
			ApplyIncludeProperties(includeProperties, ref query);

			#endregion

			if (orderBy == null)
			{
				if (!take.HasValue) return await query.ToListAsync();
				return await query.OrderBy(x => x.Version)
								  .Skip(skip * take.Value)
								  .Take(take.Value)
								  .ToListAsync();
			}
			if (!take.HasValue)
				return await orderBy.Invoke(query).ToListAsync();
			return await orderBy.Invoke(query)
								.Skip(skip*take.Value)
								.Take(take.Value)
								.ToListAsync();
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

		public async Task<int> Count()
		{
			var result = await DbSet.CountAsync();
			return result;
		}
	}
}