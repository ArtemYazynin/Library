using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Services.Impls.Services
{
	public class InvoicesService:IInvoicesService
	{
		private readonly IUnitOfWork _unitOfWork;
		public InvoicesService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public Task<IEnumerable<InvoiceDto>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<InvoiceDto> Delete(long id)
		{
			throw new NotImplementedException();
		}

		public Task<InvoiceDto> Update(long id, InvoiceDto dto)
		{
			throw new NotImplementedException();
		}

		public Task<InvoiceDto> Create(long id, InvoiceDto dto)
		{
			throw new NotImplementedException();
		}
	}
}