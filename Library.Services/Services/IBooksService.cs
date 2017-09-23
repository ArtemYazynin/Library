using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;
using Library.Services.VO;

namespace Library.Services.Services
{
	public interface IBooksService
	{
		IEnumerable<BookDto> GetAll();

		IEnumerable<BookDto> Search(Filters filters);
		BookDto Get(long id);

		Task<EntityDto> Create(BookDto bookDto);

		Task<EntityDto> Update(long id, BookDto bookDto);

		EntityDto Delete(long id);

	}
}