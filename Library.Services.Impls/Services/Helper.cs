using System;
using System.Linq;
using System.Linq.Expressions;
using Library.Common;
using Library.ObjectModel.Models;

namespace Library.Services.Impls.Services
{
	static class Helper
	{
		public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrder<TEntity>(PagingParameterModel pagingParameterModel, 
			Func<PagingParameterModel, Expression<Func<TEntity, string>>> getFieldFunc)
			where TEntity: Entity
		{
			if (pagingParameterModel == null) return null;
			var expr = getFieldFunc(pagingParameterModel);
			if (expr == null) return null;
			if (pagingParameterModel.OrderBy == OrderBy.Asc) return x => x.OrderBy(expr);
			return x => x.OrderByDescending(expr);
		}
		
	}
}