using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services.Services
{
	public interface IEditionsService
	{
		Task<IEnumerable<EditionDto>> GetAll();
	}
}