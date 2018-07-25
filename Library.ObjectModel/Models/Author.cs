using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Author: Entity, IAuthor<Book>
	{
		private string _lastname;
		private string _firstname;
		private readonly ICollection<Book> _books;

		protected Author()
		{
			_books = new List<Book>();
		}

		public Author(string lastname, string firstname, string middlename=null)
		{
			if (string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(firstname))
			{
				throw  new ArgumentNullException("Author has incorrect lastname/firstname");
			}
			_lastname = lastname;
			_firstname = firstname;
			Middlename = middlename;
			_books = new List<Book>();
		}

		public string Lastname
		{
			get { return _lastname; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_lastname = value;
				}
			}
		}

		public string Firstname
		{
			get { return _firstname; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_firstname = value;
				}
			}
		}

		public string Middlename { get; set; }

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

		public override string ToString()
		{
			return $"{Lastname} {Firstname} {Middlename ?? string.Empty}";
		}
	}
}