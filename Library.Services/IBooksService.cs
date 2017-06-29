using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services
{
	public interface IBooksService
	{
		Task<IEnumerable<BookDto>> Get();
	}
}