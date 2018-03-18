using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Common;
using Library.Services.DTO;
using Library.Services.VO;

namespace Library.Services.Services
{
	public interface IBooksService
	{
		Task<IEnumerable<BookDto>> GetAll(PagingParameterModel pagingParameterModel);

		Task<IEnumerable<BookDto>> Search(Filters filters);
		Task<BookDto> Get(long id);

		Task<EntityDto> Create(BookDto bookDto);

		Task<EntityDto> Update(long id, BookDto bookDto);

		Task<EntityDto> Delete(long id);

		Task<IEnumerable<string>> BooksByAuthor(long id);

		Task<long> Count();
	}
}