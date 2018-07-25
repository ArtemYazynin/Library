using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services;
using Moq;

namespace Library.Tests.Stubs
{
	abstract class RepositoryStubBase<TEntity> where TEntity:Entity
	{
		protected IEnumerable<TEntity> GetAllStub(IEnumerable<TEntity> entities, IEnumerable<Expression<Func<TEntity, bool>>> filters,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order)
		{

			if (filters != null)
			{
				foreach (var expression in filters)
				{
					entities = entities.Where(expression.Compile());
				}
			}
			return order?.Invoke(entities.AsQueryable()) ?? entities;
		}

		public abstract Mock<IGenericRepository<TEntity>> Get();
	}
}