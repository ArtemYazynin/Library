using System;

namespace Library.Services.Impls.Exceptions.Subscriber
{
	public class SubscriberHasRentsException :Exception
	{
		public SubscriberHasRentsException(string fio):base($"Subscriber {fio} has Rents")
		{
		}
	}
}