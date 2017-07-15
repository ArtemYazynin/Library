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
		bool Create(TEntity entity);
		bool Delete(long id);
		bool Delete(TEntity entity);
		bool Update(TEntity entity);
	}
}