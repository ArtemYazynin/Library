using System;
using Library.Services.DTO;

namespace Library.Services.Impls.Exceptions.Publisher
{
	public class PublisherIncorrectException: Exception
	{
		public PublisherIncorrectException(): base($"{nameof(PublisherDto.Name)} is required field")
		{
		}
	}
}