using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.ObjectModel.Models;
using Library.Services;
using Moq;

namespace Library.Tests.Stubs
{
	class InvoicesRepositoryStub : RepositoryStubBase<Invoice>
	{
		private readonly IList<Invoice> _invoices;

		public InvoicesRepositoryStub(IList<Invoice> invoices)
		{
			_invoices = invoices;
		}

		public override Mock<IGenericRepository<Invoice>> Get()
		{
			var stub = new Mock<IGenericRepository<Invoice>>();
			stub.Setup(x => x.GetAllAsync(It.IsAny<IEnumerable<Expression<Func<Invoice, bool>>>>(),
				It.IsAny<Func<IQueryable<Invoice>, IOrderedQueryable<Invoice>>>(),
				It.IsAny<string>(), 0, null))
				.ReturnsAsync((IEnumerable<Expression<Func<Invoice, bool>>> filters,
					Func<IQueryable<Invoice>, IOrderedQueryable<Invoice>> orders
					, string includeProperties, int skip, int? take) => GetAllStub(_invoices, filters, orders));

			stub.Setup(x => x.Get(It.IsAny<long>(), It.IsAny<string>()))
				.ReturnsAsync((long id, string includeProperties) =>
				{
					return _invoices.SingleOrDefault(x => x.Id == id);
				});
			stub.Setup(x => x.Create(It.IsAny<Invoice>()))
				.Returns((Invoice x) =>
				{
					_invoices.Add(x);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<long>()))
				.Returns((long id) =>
				{
					var deletedInvoice = _invoices.SingleOrDefault(x => x.Id == id);
					if (deletedInvoice == null)
					{
						return false;
					}

					_invoices.Remove(deletedInvoice);
					return true;
				});
			stub.Setup(x => x.Delete(It.IsAny<Invoice>()))
				.Returns((Invoice x) =>
				{
					try
					{
						_invoices.Remove(x);
						return true;
					}
					catch (Exception)
					{

						return false;
					}
				});
			stub.Setup(x => x.Update(It.IsAny<Invoice>()))
				.Returns((Invoice x) =>
				{
					try
					{
						var invoice = _invoices.Single(n => n.Id == x.Id);
						invoice.Date = x.Date;
						invoice.IncomingBooks = x.IncomingBooks;
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