using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface ISubscribersService
	{
		Task<IEnumerable<SubscriberDto>> GetAll();
		Task<SubscriberDto> Delete(long id);
		Task<SubscriberDto> Update(long id, SubscriberDto dto);
		Task<SubscriberDto> Create(SubscriberDto dto);
	}
}