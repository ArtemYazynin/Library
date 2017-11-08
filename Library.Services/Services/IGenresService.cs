using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface IGenresService
	{
		Task<IEnumerable<GenreDto>> GetAll();

		Task<EntityDto> Delete(long id, bool recursivelly);

		Task<GenreDto> Update(long id, GenreDto dto);
		Task<GenreDto> Create(GenreDto dto);
	}
}