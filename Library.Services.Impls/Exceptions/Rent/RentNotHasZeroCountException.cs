using System;

namespace Library.Services.Impls.Exceptions.Rent
{
	public class RentNotHasZeroCountException : Exception
	{
		public RentNotHasZeroCountException():base("Rent has invalid Count value")
		{
		}
	}
}