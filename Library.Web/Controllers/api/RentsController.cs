using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Common;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Web.Controllers.api
{
	[RoutePrefix("api/Rents")]
	public class RentsController:ApiController
	{
		private readonly IRentsService _rentsService;

		public RentsController(IRentsService rentsService)
		{
			_rentsService = rentsService;
		}

		public async Task<IEnumerable<RentDto>> Get([FromUri]PagingParameterModel model)
		{
			var rents = await _rentsService.GetAll(model);
			await this.AddTotalItemsInHeader(_rentsService.Count);
			return rents;
		}

		public async Task<RentDto> Post(RentDto dto)
		{
			return await _rentsService.Create(dto);
		}

		public async Task<RentDto> Delete(long id)
		{
			return await _rentsService.Delete(id);
		}

		public async Task<RentDto> Put(long id, RentDto dto)
		{
			return await _rentsService.Update(id, dto);
		}
	}
}