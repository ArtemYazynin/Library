using System.Collections.Generic;
using Library.Services.DTO;

namespace Library.Services
{
	public interface IBooksService
	{
		IEnumerable<BookDto> Get();
	}
}