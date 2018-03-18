using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services;
using Moq;

namespace Library.Tests.Stubs
{
	class PublishersRepositoryStub:RepositoryStubBase<Publisher>
	{
		private readonly IList<Publisher> _publishers;

		public PublishersRepositoryStub(IList<Publisher> publishers)
		{
			_publishers = publishers;
		}

		public override Mock<IGenericRepository<Publisher>> Get()
		{
			var stub = new Mock<IGenericRepository<Publisher>>();
			stub.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Publisher, bool>>>>(),
				It.IsAny<Func<IQueryable<Publisher>, IOrderedQueryable<Publisher>>>(),
				It.IsAny<string>(), 0, null))
				.ReturnsAsync((IEnumerable<Expression<Func<Publisher, bool>>> filters,
					Func<IQueryable<Publisher>, IOrderedQueryable<Publisher>> orders
					, string includeProperties, int skip, int? take) => GetAllStub(_publishers, filters, orders));

			stub.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) =>
				{
					return _publishers.SingleOrDefault(x => x.Id == id);
				});
			stub.Setup(x => x.Create(It.IsAny<Publisher>()))
				.Returns((Publisher x) =>
				{
					_publishers.Add(x);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var deletedPublisher = _publishers.SingleOrDefault(x => x.Id == id);
					if (deletedPublisher == null)
					{
						return false;
					}

					_publishers.Remove(deletedPublisher);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<Publisher>()))
				.Returns((Publisher x) =>
				{
					try
					{
						_publishers.Remove(x);
						return true;
					}
					catch (Exception)
					{

						return false;
					}
				});
			stub.Setup(x => x.Update(It.IsAny<Publisher>()))
				.Returns((Publisher x) =>
				{
					try
					{
						var publisher = _publishers.Single(n => n.Id == x.Id);
						publisher.Name = x.Name;

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