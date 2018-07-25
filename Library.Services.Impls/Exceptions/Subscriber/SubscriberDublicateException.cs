using System;

namespace Library.Services.Impls.Exceptions.Subscriber
{
	public class SubscriberDublicateException : Exception
	{
		public SubscriberDublicateException() : base("Subscriber with same Lastname/Firstname/Middlename already exists")
		{
		}
	}
}