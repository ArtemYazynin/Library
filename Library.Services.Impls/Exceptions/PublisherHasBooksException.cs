using System;
using System.Collections.Generic;
using System.Linq;
using Library.ObjectModel.Models;

namespace Library.Services.Impls.Exceptions
{
	public class PublisherHasBooksException : Exception
	{
		public PublisherHasBooksException(IEnumerable<Book> books) : base($"Publisher using in these books: {string.Join(", ", books.Select(x=>x.Name))}")
		{
		}
	}
}