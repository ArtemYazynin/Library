using System;
using Library.Services.DTO;

namespace Library.Services.Impls.Exceptions
{
	public class GenreIncorrectException: Exception
	{
		public GenreIncorrectException() : base($"{nameof(GenreDto.Name)} is required field")
		{
		}
	}
}