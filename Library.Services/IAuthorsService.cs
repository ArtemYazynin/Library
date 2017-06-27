using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services
{
	public interface IAuthorsService
	{
		Task<IEnumerable<AuthorDto>> Get();
		IEnumerable<AuthorDto> Get(long id);
	}
}
