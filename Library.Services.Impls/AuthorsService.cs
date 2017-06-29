﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Library.ObjectModel.Models;
using Library.Services.DTO;

namespace Library.Services.Impls
{
	public class BooksService : IBooksService
	{
		public async Task<IEnumerable<BookDto>> Get()
		{
			using (var db = new LibraryContext())
			{
				var books = await db.Books.ToListAsync();
				var result = books.Select(x => new BookDto() {Name = x.Name});
				return result;
			}
		}
	}
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
