using System;

namespace Library.Services.Impls.Exceptions.Publisher
{
	public class PublisherDublicateException : Exception
	{
		public PublisherDublicateException(): base("Publisher with same name already exists")
		{
		}
	}
}