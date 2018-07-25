using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Edition: Entity, IEdition<Book>
	{
		private string _name;
		private int _year;
		private readonly ICollection<Book> _books;

		protected Edition()
		{
			_books = new List<Book>();
		}

		public Edition(string name, int year)
		{
			_name = name;
			_year = year;
			_books = new List<Book>();
		}

		public string Name
		{
			get { return _name; }
			set 
			{
				if (!string.IsNullOrEmpty(value))
				{
					_name = value;
				}
			}
		}

		public int Year
		{
			get { return _year; }
			set
			{
				if (value != default(int))
				{
					_year = value;
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
						if (book!=null)
						{
							_books.Add(book);
						}
					}
				}
			}
		}
	}
}