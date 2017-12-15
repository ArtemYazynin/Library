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
				var book = await _unitOfWork.BookRepository.Get(incomingBook.Book.Id);
				book.Count = book.Count - incomingBook.Count;
				_unitOfWork.BookRepository.Update(book);
				await _unitOfWork.Save();
			}
			_unitOfWork.InvoiceRepository.Delete(invoice);
			await _unitOfWork.Save();
			return new InvoiceDto() {Id = id};
		}

		public Task<InvoiceDto> Update(long id, InvoiceDto dto)
		{
			throw new NotImplementedException();
		}

		public async Task<InvoiceDto> Create(InvoiceDto dto)
		{
			foreach (var incomingBook in dto.IncomingBooks)
			{
				var book = await _unitOfWork.BookRepository.Get(incomingBook.Book.Id);
				book.Count = book.Count + incomingBook.Count;
				_unitOfWork.BookRepository.Update(book);
				await _unitOfWork.Save();
			}
			var invoice = Mapper.Map<Invoice>(dto);
			_unitOfWork.InvoiceRepository.Create(invoice);
			await _unitOfWork.Save();
			return Mapper.Map<InvoiceDto>(invoice);
		}
	}
}