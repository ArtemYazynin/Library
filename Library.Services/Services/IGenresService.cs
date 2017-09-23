using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface IGenresService
	{
		Task<IEnumerable<GenreDto>> GetAll();
	}
}