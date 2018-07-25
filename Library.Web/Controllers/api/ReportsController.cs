using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Services;
using Newtonsoft.Json.Linq;

namespace Library.Web.Controllers.api
{
	[RoutePrefix("api/Reports")]
	public class ReportsController:ApiController
	{
		private readonly IReportsService _reportsService;

		public ReportsController(IReportsService reportsService)
		{
			_reportsService = reportsService;
		}

		[HttpGet]
		[Route("MostPopular")]
		public async Task<IEnumerable<MostPopularReportRow>> MostPopular(DateTime from ,DateTime to)
		{
			var result = await _reportsService.MostPopular(from, to);
			return result;
		}
	}
}