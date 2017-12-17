using System;

namespace Library.Services.Impls.Exceptions.Subscriber
{
	public class SubscriberHasIncorrectIdException: Exception
	{
		public SubscriberHasIncorrectIdException(long id):base($"Updated subscriber has incorrect ID: {id}")
		{
		}
	}
}