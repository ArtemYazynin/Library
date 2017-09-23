using System.Collections.Generic;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface IPublishersService
	{
		IEnumerable<PublisherDto> GetAll();
	}
}