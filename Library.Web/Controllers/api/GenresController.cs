using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Library.Services.DTO;
using Library.Services.Services;
using Microsoft.Ajax.Utilities;

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

		public async Task<IEnumerable<GenreSimpleDto>> Get()
		{
			var genres = await _genresService.GetAll();
			return genres;
		}
		[Route("GetTree")]
		public async Task<IEnumerable<GenreDto>> GetTree()
		{
			var genres = await _genresService.GetTree();
			return genres;
		}

		[HttpDelete]
		public async Task<EntityDto> Delete(long id)
		{
			var result = await _genresService.Delete(id, true);
			return result;
		}

		[HttpPut]
		public async Task<EntityDto> Put(long id, GenreDto dto)
		{
			var result = await _genresService.Update(id, dto);
			return result;
		}

		[HttpPost]
		public async Task<GenreDto> Post(GenreDto dto)
		{
			return await _genresService.Create(dto);
		}
	}
}