using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Common;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface IAuthorsService
	{
		Task<IEnumerable<AuthorDto>> GetAll(PagingParameterModel pagingParameterModel);
		Task<EntityDto> Delete(long id);
		Task<AuthorDto> Get(long id);

		Task<AuthorDto> Update(long id, AuthorDto authorDto);
		Task<EntityDto> Create(AuthorDto authorDto);
		Task<long> Count();
	}
}
