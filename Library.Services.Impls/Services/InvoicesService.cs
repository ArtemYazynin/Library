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
		private readonly IBooksService _booksService;
		private readonly string _includeProperties = $"{nameof(Invoice.IncomingBooks)}.{nameof(Book)}";
		public InvoicesService(IUnitOfWork unitOfWork, IBooksService booksService)
		{
			_unitOfWork = unitOfWork;
			_booksService = booksService;
		}
		public async Task<IEnumerable<InvoiceDto>> GetAll()
		{
			var result = await _unitOfWork.InvoiceRepository.GetAllAsync(null, x => x.OrderBy(y => y.Date), _includeProperties);
			return Mapper.Map<IEnumerable<InvoiceDto>>(result);
		}

		public async Task<InvoiceDto> Delete(long id)
		{
			var invoice = await _unitOfWork.InvoiceRepository.Get(id, _includeProperties);

			foreach (var incomingBook in invoice.IncomingBooks)
			{
				var dto = Mapper.Map<BookDto>(incomingBook.Book);
				dto.Count = dto.Count - incomingBook.Count;
				await _booksService.Update(dto.Id, dto);
			}
			_unitOfWork.InvoiceRepository.Delete(invoice);
			return new InvoiceDto() {Id = id};
		}

		public Task<InvoiceDto> Update(long id, InvoiceDto dto)
		{
			throw new NotImplementedException();
		}

		public Task<InvoiceDto> Create(InvoiceDto dto)
		{
			throw new NotImplementedException();
		}
	}
}