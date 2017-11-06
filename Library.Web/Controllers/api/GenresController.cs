using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Services.DTO;
using Library.Services.Services;

namespace Library.Web.Controllers.api
{
	[RoutePrefix("api/Genres")]
	public class GenresController: ApiController
	{
		private readonly IGenresService _genresService;

		public GenresController(IGenresService genresService)
		{
			_genresService = genresService;
		}

		public async Task<IEnumerable<GenreDto>> Get()
		{
			var genres = await _genresService.GetAll();
			return genres;
		}

		[HttpDelete]
		public async Task<EntityDto> Delete(long id)
		{
			var result = await _genresService.Delete(id, true);
			return result;
		} 
	}
}