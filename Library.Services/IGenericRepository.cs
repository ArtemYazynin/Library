using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Services
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "");

		TEntity Get(long id);
		void Create(TEntity entity);
		void Delete(long id);
		void Delete(TEntity entity);
		void Update(TEntity entity);
	}
}