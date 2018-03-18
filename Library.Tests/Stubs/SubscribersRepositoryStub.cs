using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services;
using Moq;

namespace Library.Tests.Stubs
{
	class SubscribersRepositoryStub : RepositoryStubBase<Subscriber>
	{
		private readonly IList<Subscriber> _subscribers;

		public SubscribersRepositoryStub(IList<Subscriber> subscribers)
		{
			_subscribers = subscribers;
		}

		public override Mock<IGenericRepository<Subscriber>> Get()
		{
			var stubSubscriberRepository = new Mock<IGenericRepository<Subscriber>>();
			stubSubscriberRepository.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Subscriber, bool>>>>(),
				It.IsAny<Func<IQueryable<Subscriber>, IOrderedQueryable<Subscriber>>>(),
				It.IsAny<string>(), 0, null))
				.ReturnsAsync((IEnumerable<Expression<Func<Subscriber, bool>>> filters,
					Func<IQueryable<Subscriber>, IOrderedQueryable<Subscriber>> order, string includeProperties, int skip, int? take) =>
				{
					IEnumerable<Subscriber> localEntities = _subscribers;
					return GetAllStub(localEntities, filters, order);
				});

			stubSubscriberRepository.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) => _subscribers.SingleOrDefault(x => x.Id == id));

			stubSubscriberRepository.Setup(x => x.Create(It.IsAny<Subscriber>()))
				.Returns((Subscriber x) =>
				{
					_subscribers.Add(x);
					return true;
				});
			stubSubscriberRepository.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var subscriber = _subscribers.SingleOrDefault(x => x.Id == id);
					if (subscriber == null) return false;

					_subscribers.Remove(subscriber);
					return true;
				});
			stubSubscriberRepository.Setup(x => x.Delete(It.IsAny<Subscriber>()))
				.Returns<Subscriber>(x =>
				{
					_subscribers.Remove(x);
					return true;
				});
			stubSubscriberRepository.Setup(x => x.Update(It.IsAny<Subscriber>())).Returns((Subscriber dbentity) =>
			{
				var subscriber = _subscribers.Single(x => x.Id == dbentity.Id);
				subscriber.Lastname = dbentity.Lastname;
				subscriber.Firstname = dbentity.Firstname;
				subscriber.Middlename = dbentity.Middlename;

				return true;
			});
			return stubSubscriberRepository;
		}
	}
}