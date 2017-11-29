using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface IInvoicesService
	{
		Task<IEnumerable<InvoiceDto>> GetAll();
		Task<InvoiceDto> Delete(long id);
		Task<InvoiceDto> Update(long id, InvoiceDto dto);
		Task<InvoiceDto> Create(long id, InvoiceDto dto);
	}
}