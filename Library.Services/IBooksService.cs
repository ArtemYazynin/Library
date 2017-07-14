using System.Collections.Generic;
using Library.Services.DTO;

namespace Library.Services
{
	public interface IBooksService
	{
		IEnumerable<BookDto> GetAll();

		BookDto Get(long id);

		EntityDto Create(BookDto bookDto);

		EntityDto Update(long id, BookDto bookDto);

		EntityDto Delete(long id);
	}
}