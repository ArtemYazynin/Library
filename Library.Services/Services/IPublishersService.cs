using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface IPublishersService
	{
		Task<IEnumerable<PublisherDto>> GetAll();
		Task<PublisherDto> Delete(long id);

		Task<PublisherDto> Update(long id, PublisherDto dto);
		Task<PublisherDto> Create(PublisherDto dto);
	}
}