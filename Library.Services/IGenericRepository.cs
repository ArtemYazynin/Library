using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.Services
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters = null, 
											   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
											   string includeProperties = "",
											   int skip = 0,
											   int? take = null);
		Task<TEntity> Get(long id, string includeProperties = "");
		bool Create(TEntity entity);
		bool Delete(long id);
		bool Delete(TEntity entity);
		bool Update(TEntity entity);

		Task<int> Count();
	}
}