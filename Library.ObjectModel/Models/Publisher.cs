using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Publisher : Entity, IPublisher<Book>
	{
		private string _name;
		private readonly ICollection<Book> _books;

		public Publisher(string name)
		{
			_name = name;
			_books = new List<Book>();
		}

		protected Publisher()
		{
			_books = new List<Book>();
		}

		public string Name
		{
			get { return _name; }
			set {
				if (!string.IsNullOrEmpty(value))
				{
					_name = value;
				}
			}
		}

		public ICollection<Book> Books
		{
			get { return _books; }
			set
			{
				if (value == null)
				{
					_books.Clear();
				}
				else
				{
					foreach (var book in value)
					{
						if (book != null)
						{
							_books.Add(book);
						}
					}
				}
			}
		}
	}
}