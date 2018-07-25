using System;

namespace Library.Services.Impls.Exceptions.Subscriber
{
	public class SubscriberIncorrectException : Exception
	{
		public SubscriberIncorrectException() : base("Lastname/Firstname is required fields")
		{
		}
	}
}