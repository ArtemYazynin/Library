using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Web.Controllers.api
{
	[RoutePrefix("api/Invoices")]
	public class InvoicesController: ApiController
	{
		private readonly IInvoicesService _invoicesService;

		public InvoicesController(IInvoicesService invoicesService)
		{
			_invoicesService = invoicesService;
		}

		public async Task<IEnumerable<InvoiceDto>> Get()
		{
			var result = await _invoicesService.GetAll();
			return result;
		}

		public async Task<InvoiceDto> Delete(long id)
		{
			var result = await _invoicesService.Delete(id);
			return result;
		}

		public async Task Post(InvoiceDto dto)
		{
			await _invoicesService.Create(dto);
		}
	}
}