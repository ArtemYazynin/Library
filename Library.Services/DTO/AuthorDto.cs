﻿using System.Collections.Generic;
using Library.ObjectModel.Models;

namespace Library.Services.DTO
{
	public class AuthorDto
	{
		public AuthorDto()
		{
			Books = new List<Book>();
		}

		public string Lastname { get; set; }
		public string Firstname { get; set; }
		public string Middlename { get; set; }
		public IEnumerable<Book> Books { get; set; }
	}
}
