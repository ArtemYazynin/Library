using System;
using System.Collections.Generic;

namespace Library.Services.Impls.Exceptions.Genre
{
	public class GenreIsUsedException : Exception
	{
		public GenreIsUsedException(IEnumerable<string> books):base($"Genre has used in books: {string.Join(", ",books)}")
		{
		}
	}
}