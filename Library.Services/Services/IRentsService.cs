using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface IRentsService
	{
		Task<IEnumerable<RentDto>> GetAll();
		Task<RentDto> GetById(long id);
		Task<RentDto> Delete(long id);
		Task<RentDto> Update(long id, RentDto dto);
		Task<RentDto> Create(RentDto dto);
	}
}