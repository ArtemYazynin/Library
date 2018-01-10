using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Book: Entity, IBook<Edition,Publisher,Genre,Author,Rent, IncomingBook, File>
	{
		private string _name;
		private string _isbn;
		private Edition _edition;
		private Publisher _publisher;
		private readonly ICollection<Genre> _genres;
		private readonly ICollection<Author> _authors;
		private readonly ICollection<Rent> _rents;
		private readonly ICollection<IncomingBook> _incomingBooks;

		protected Book()
		{
			_authors = new List<Author>();
			_rents = new List<Rent>();
			_genres = new List<Genre>();
			_incomingBooks = new List<IncomingBook>();
		}
		public Book(string name, string isbn, Edition edition, Publisher publisher, int count)
		{
			_name = name;
			_isbn = isbn;
			_edition = edition;
			_publisher = publisher;
			Count = count;

			_authors = new List<Author>();
			_rents = new List<Rent>();
			_genres = new List<Genre>();
			_incomingBooks = new List<IncomingBook>();
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

		public string Isbn
		{
			get { return _isbn; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_isbn = value;
				}
			}
		}

		public string Description { get; set; }

		public int Count { get; set; }

		public Edition Edition
		{
			get { return _edition; }
			set
			{
				if (value != null)
				{
					_edition = value;
				}
			}
		}

		public long EditionId { get; set; }

		public Publisher Publisher
		{
			get { return _publisher; }
			set
			{
				if (value != null)
				{
					_publisher = value;
				}
			}
		}

		public long PublisherId { get; set; }

		public File Cover { get; set; }
		public long? CoverId { get; set; }

		public ICollection<Genre> Genres
		{
			get { return _genres; }
			set
			{
				if (value == null)
				{
					_genres.Clear();
				}
				else
				{
					foreach (var genre in value)
					{
						if (genre != null)
						{
							_genres.Add(genre);
						}
					}
				}
			}
		}

		public ICollection<Author> Authors
		{
			get { return _authors; }
			set
			{
				if (value == null)
				{
					_authors.Clear();
				}
				else
				{
					foreach (var author in value)
					{
						if (author != null)
						{
							_authors.Add(author);
						}
					}
				}
				
			}
		}

		public ICollection<Rent> Rents
		{
			get { return _rents; }
			set
			{
				if (value == null)
				{
					_rents.Clear();
				}
				else
				{
					foreach (var rent in value)
					{
						if (rent != null)
						{
							_rents.Add(rent);
						}
					}
				}
				
			}
		}

		public ICollection<IncomingBook> IncomingBooks
		{
			get { return _incomingBooks; }
			set
			{
				if (value == null)
				{
					_incomingBooks.Clear();
				}
				else
				{
					foreach (var incomingBook in value)
					{
						if (incomingBook != null)
						{
							_incomingBooks.Add(incomingBook);
						}
					}
				}
			}
		}
	}
}
