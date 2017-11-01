using System;

namespace Library.Services.Impls.Exceptions
{
	public class AuthorDublicateException :Exception
	{
		public AuthorDublicateException():base("Author with same Lastname/Firstname/Middlename already exists")
		{
		}
	}
}