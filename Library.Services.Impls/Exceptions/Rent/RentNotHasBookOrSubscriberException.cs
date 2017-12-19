using System;

namespace Library.Services.Impls.Exceptions.Rent
{
	public class RentNotHasBookOrSubscriberException : Exception
	{
		public RentNotHasBookOrSubscriberException():base("Rent not has book or subscriber")
		{
		}
	}
}