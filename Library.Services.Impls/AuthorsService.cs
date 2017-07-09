using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Library.Services.DTO;

namespace Library.Services.Impls
{
	public class AuthorsService: IAuthorsService
	{
		public async Task<IEnumerable<AuthorDto>> Get()
		{
			using (var db = new LibraryContext())
			{
				var authors =await db.Authors.ToListAsync();
				var result = authors.Select(x => new AuthorDto()
				{
					Lastname = x.Lastname,
					Firstname = x.Firstname,
					Middlename = x.Middlename,
				});
				return result;
			}
		}

		public IEnumerable<AuthorDto> Get(long id)
		{
			throw new NotImplementedException();
		}
	}
}
