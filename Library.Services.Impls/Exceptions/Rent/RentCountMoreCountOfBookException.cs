using System;

namespace Library.Services.Impls.Exceptions.Rent
{
	public class RentCountMoreCountOfBookException : Exception
	{
		public RentCountMoreCountOfBookException(string book) : base($"Rent count more count of book {book}")
		{
		}
	}
}