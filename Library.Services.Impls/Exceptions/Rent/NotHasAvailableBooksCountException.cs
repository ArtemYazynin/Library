using System;

namespace Library.Services.Impls.Exceptions.Rent
{
	public class NotHasAvailableBooksCountException : Exception
	{
		public NotHasAvailableBooksCountException():base("No books available")
		{
		}
	}
}