using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Common;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface ISubscribersService
	{
		Task<IEnumerable<SubscriberDto>> GetAll(PagingParameterModel model);
		Task<SubscriberDto> GetById(long id);
		Task<SubscriberDto> Delete(long id);
		Task<SubscriberDto> Update(long id, SubscriberDto dto);
		Task<SubscriberDto> Create(SubscriberDto dto);
		Task<long> Count();
	}
}