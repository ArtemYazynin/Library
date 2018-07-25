using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Library.ObjectModel.Models;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Services.Impls.Services
{
	public class ReportsService : IReportsService
	{
		private readonly IUnitOfWork _unitOfWork;
		private static readonly string IncludeProps = $"{nameof(Rent.Subscriber)}, {nameof(Rent.Book)}";

		public ReportsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<MostPopularReportRow>> MostPopular(DateTime from, DateTime to)
		{
			List<Expression<Func<Rent, bool>>> filters = new List<Expression<Func<Rent, bool>>>();
			filters.Add(x => x.Date >= from && x.Date <= to);
			var rents = await _unitOfWork.RentRepository.GetAllAsync(filters,includeProperties: IncludeProps);
			var grouped = rents.GroupBy(x => x.Book.Name).Select(x => new MostPopularReportRow()
			{
				BookName = x.Key,
				Count = x.Sum(r=>r.Count)
			}).OrderByDescending(x=>x.Count).Take(5).ToList();
			return grouped;
		}
	}
}