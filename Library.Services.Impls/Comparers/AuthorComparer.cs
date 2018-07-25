using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.Services.Impls.Comparers
{
	public class AuthorComparer<TEntity> : IEqualityComparer<TEntity> where TEntity: Entity
	{
		public bool Equals(TEntity x, TEntity y)
		{
			return x != null && y != null && x.Id.Equals(y.Id);
		}

		public int GetHashCode(TEntity obj)
		{
			return obj.Id.GetHashCode();
		}
	}
}