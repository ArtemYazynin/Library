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
		private readonly string _includeProperties = $"{nameof(Invoice.IncomingBooks)}.{nameof(Book)}";
		public InvoicesService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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
				if (_unitOfWork.BookRepository.Update(book))
				{
					await _unitOfWork.Save();
				}
				
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
			var invoice = new Invoice()
			{
				Date = DateTime.Now
			};
			await FillIncoimingBooks(dto, invoice);
			if (_unitOfWork.InvoiceRepository.Create(invoice))
			{
				await _unitOfWork.Save();
			}

			return Mapper.Map<InvoiceDto>(invoice);
		}

		private async Task FillIncoimingBooks(InvoiceDto dto, Invoice invoice)
		{
			foreach (var incomingBook in dto.IncomingBooks)
			{
				var book = await _unitOfWork.BookRepository.Get(incomingBook.Book.Id);
				await UpdateBookCount(book, incomingBook);
				invoice.IncomingBooks.Add(new IncomingBook()
				{
					Book = book,
					Count = incomingBook.Count,
				});
			}
		}

		private async Task UpdateBookCount(Book book, IncomingBookDto incomingBook)
		{
			book.Count = book.Count + incomingBook.Count;
			_unitOfWork.BookRepository.Update(book);
			await _unitOfWork.Save();
		}
	}
}