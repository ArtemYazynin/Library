using System.Collections.Generic;
using Library.Services.DTO;

namespace Library.Services
{
	public interface IBooksService
	{
		IEnumerable<BookDto> Get();

		IEnumerable<BookDto> GetById();

		EntityDto Create(BookDto bookDto);

		EntityDto Update(long id, BookDto bookDto);

		EntityDto Delete(long id);
	}
}