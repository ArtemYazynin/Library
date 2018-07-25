using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Genre: Entity, IGenre<Genre, Book>
	{
		private string _name;
		private readonly ICollection<Book> _books;
		private readonly ICollection<Genre> _children;

		protected Genre()
		{
			_books = new List<Book>();
			_children = new List<Genre>();
		}

		public Genre(string name)
		{
			_name = name;
			_books = new List<Book>();
			_children = new List<Genre>();
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

		public Genre Parent { get; set; }

		public long? ParentId { get; set; }

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

		public ICollection<Genre> Children
		{
			get { return _children; }
			set
			{
				if (value == null)
				{
					_children.Clear();
				}
				else
				{
					foreach (var genre in value)
					{
						if (genre != null)
						{
							_children.Add(genre);
						}
					}
				}
				
			}
		}

		public void AddChild(Genre genre)
		{
			if (genre != null)
			{
				_children.Add(genre);
			}
		}


		public override string ToString()
		{
			return $"{Name}";
		}
	}
}