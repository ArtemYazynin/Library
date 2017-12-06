using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.ObjectModel.Models;
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
		public async Task<IEnumerable<InvoiceDto>> GetAll()
		{
			var includeProperties = $"{nameof(Invoice.IncomingBooks)}.{nameof(Book)}";
			var result = await _unitOfWork.InvoiceRepository.GetAllAsync(null, x => x.OrderBy(y => y.Date), includeProperties);
			return Mapper.Map<IEnumerable<InvoiceDto>>(result);
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