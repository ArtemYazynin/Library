using System;

namespace Library.Services.Impls.Exceptions.Subscriber
{
	public class SubscriberHasActiveRentsException :Exception
	{
		public SubscriberHasActiveRentsException(string fio):base($"Subscriber {fio} has active Rents")
		{
		}
	}
}