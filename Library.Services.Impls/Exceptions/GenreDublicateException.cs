using System;

namespace Library.Services.Impls.Exceptions
{
	public class GenreDublicateException: Exception
	{
		public GenreDublicateException() : base("Genre with same Name already exists")
		{
		}
	}
}