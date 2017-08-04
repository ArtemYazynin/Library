using System.Collections.Generic;
using Library.Services.DTO;
using Library.Services.VO;

namespace Library.Services
{
	public interface IBooksService
	{
		IEnumerable<BookDto> GetAll();

		IEnumerable<BookDto> Search(Filters filters);
		BookDto Get(long id);

		EntityDto Create(BookDto bookDto);

		EntityDto Update(long id, BookDto bookDto);

		EntityDto Delete(long id);

	}
}