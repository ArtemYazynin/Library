using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services.VO;
using LinqKit;

namespace Library.Services.Impls.Services.Builders
{
	class ExpressionBuilder : AbstractExpressionBuilder
	{
		private readonly IList<Expression<Func<Book, bool>>> _expressions = new List<Expression<Func<Book, bool>>>();
		private readonly Filters _filters;
		public ExpressionBuilder(Filters filters)
		{
			_filters = filters;
		}

		public override void FilterByName()
		{
			if (!string.IsNullOrEmpty(_filters.ByName))
			{
				_expressions.Add(x => x.Name.ToLower().Contains(_filters.ByName.ToLower()));
			}
		}

		public override void FilterByAuthor()
		{
			if (!string.IsNullOrEmpty(_filters.ByAuthor))
			{
				var criterion = _filters.ByAuthor.Trim().ToLower();
				_expressions.Add(GetByPersonExpression(criterion));
			}
		}

		public override void FilterByAuthors()
		{
			if (!string.IsNullOrEmpty(_filters.ByMultipleAuthors))
			{
				var groupOfCriteria = _filters.ByMultipleAuthors.ToLower().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
				Expression<Func<Book, bool>> predicate = c => false;
				foreach (var criterion in groupOfCriteria)
				{
					predicate = predicate.Or(GetByPersonExpression(criterion));
				}
				_expressions.Add(predicate);
			}
		}

		public override void FilterByAll()
		{
			if (!string.IsNullOrEmpty(_filters.ByAll))
			{
				Expression<Func<Book, bool>> predicate = c => false;
				var groupOfCriteria = _filters.ByAll.ToLower().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
				foreach (var criterion in groupOfCriteria)
				{
					predicate = predicate.Or(x => x.Name.ToLower().Contains(criterion)
					                              || !string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(criterion)
					                              || x.Authors.Any(a => string.IsNullOrEmpty(a.Middlename)
						                              ? a.Lastname.ToLower().Contains(criterion) || a.Firstname.ToLower().Contains(criterion)
						                              : a.Lastname.ToLower().Contains(criterion) || a.Firstname.ToLower().Contains(criterion)
						                                || a.Middlename.ToLower().Contains(criterion)));
				}
				_expressions.Add(predicate);
			}
		}

		public override void FilterByWithoutAuthors()
		{
			if (_filters.WithoutAuthors)
			{
				_expressions.Add(x => !x.Authors.Any());
			}
		}

		public override IList<Expression<Func<Book, bool>>> GetResult()
		{
			return _expressions;
		}

		private Expression<Func<Book, bool>> GetByPersonExpression(string criterion)
		{
			return x => x.Authors.Any(a => string.IsNullOrEmpty(a.Middlename)
				? a.Lastname.ToLower().Contains(criterion) || a.Firstname.ToLower().Contains(criterion)
				: a.Lastname.ToLower().Contains(criterion) || a.Firstname.ToLower().Contains(criterion)
				  || a.Middlename.ToLower().Contains(criterion));
		}
	}
}