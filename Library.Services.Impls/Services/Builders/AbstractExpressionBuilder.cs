using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Library.ObjectModel.Models;

namespace Library.Services.Impls.Services.Builders
{
	abstract class AbstractExpressionBuilder
	{
		public abstract void FilterByName();
		public abstract void FilterByAuthor();
		public abstract void FilterByAuthors();
		public abstract void FilterByAll();
		public abstract void FilterByWithoutAuthors();
		public abstract IList<Expression<Func<Book, bool>>> GetResult();
	}
}