using System;

namespace Library.Services.Impls.Exceptions
{
	public class AuthorIncorrectException: Exception
	{
		public AuthorIncorrectException():base("Lastname/Firstname is required fields")
		{
		}
	}
}