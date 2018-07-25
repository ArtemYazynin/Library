using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.DefaultData;
using Library.ObjectModel.Models;
using Library.Services;
using Moq;

namespace Library.Tests.Stubs
{
	class RentsRepositoryStub : RepositoryStubBase<Rent>
	{
		private readonly IList<Rent> _rents;

		public RentsRepositoryStub(IList<Rent> rents)
		{
			_rents = rents;
		}


		public override Mock<IGenericRepository<Rent>> Get()
		{
			var stub = new Mock<IGenericRepository<Rent>>();
			stub.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Rent, bool>>>>(),
				It.IsAny<Func<IQueryable<Rent>, IOrderedQueryable<Rent>>>(),
				It.IsAny<string>(), 0, null))
				.ReturnsAsync((IEnumerable<Expression<Func<Rent, bool>>> filters,
					Func<IQueryable<Rent>, IOrderedQueryable<Rent>> orders
					, string includeProperties, int skip, int? take) => GetAllStub(_rents, filters, orders));

			stub.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) =>
				{
					return _rents.SingleOrDefault(x => x.Id == id);
				});
			stub.Setup(x => x.Create(It.IsAny<Rent>()))
				.Returns((Rent x) =>
				{
					_rents.Add(x);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var deletedRent = _rents.SingleOrDefault(x => x.Id == id);
					if (deletedRent == null)
					{
						return false;
					}

					_rents.Remove(deletedRent);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<Rent>()))
				.Returns((Rent x) =>
				{
					try
					{
						_rents.Remove(x);
						return true;
					}
					catch (Exception)
					{

						return false;
					}
				});
			stub.Setup(x => x.Update(It.IsAny<Rent>()))
				.Returns((Rent x) =>
				{
					try
					{
						var rent = _rents.Single(n => n.Id == x.Id);
						rent.IsActive = x.IsActive;

						return true;
					}
					catch (Exception)
					{
						return false;
					}
				});
			return stub;
		}
	}
}