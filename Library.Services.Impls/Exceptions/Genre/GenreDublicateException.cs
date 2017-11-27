using System;

namespace Library.Services.Impls.Exceptions.Genre
{
	public class GenreDublicateException: Exception
	{
		public GenreDublicateException() : base("Genre with same name already exists")
		{
		}
	}
}