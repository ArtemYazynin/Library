using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.ObjectModel.Models;

namespace Library.Services
{
	public interface IGenresRepository: IGenericRepository<Genre>
	{
		Task<IEnumerable<Genre>> GetTree(IList<Expression<Func<Genre, bool>>> filters);
	}
}