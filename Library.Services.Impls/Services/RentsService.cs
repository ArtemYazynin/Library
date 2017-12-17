using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Services.Impls.Services
{
	public class RentsService : IRentsService
	{
		private readonly IUnitOfWork _unitOfWork;

		public RentsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public Task<IEnumerable<RentDto>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<RentDto> GetById(long id)
		{
			throw new NotImplementedException();
		}

		public Task<RentDto> Delete(long id)
		{
			throw new NotImplementedException();
		}

		public Task<RentDto> Update(long id, RentDto dto)
		{
			throw new NotImplementedException();
		}

		public Task<RentDto> Create(RentDto dto)
		{
			throw new NotImplementedException();
		}
	}
}