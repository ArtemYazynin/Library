using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface IReportsService
	{
		Task<IEnumerable<MostPopularReportRow>> MostPopular(DateTime from , DateTime to);
	}
}