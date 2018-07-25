using System;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Rent:Entity, IRent<Book, Subscriber>
	{
		#region private fields

		private Book _book;
		private Subscriber _subscriber;

		#endregion


		#region .ctors

		protected Rent()
		{
			Date = DateTime.Now;
		}

		public Rent(Book book, Subscriber subscriber, int count = 0, bool isActive = false)
		{
			_book = book;
			_subscriber = subscriber;
			Count = count;
			IsActive = isActive;
			Date = DateTime.Now;
		}

		public Rent(DateTime date, Book book, Subscriber subscriber, int count = 0, bool isActive = false)
		{
			_book = book;
			_subscriber = subscriber;
			Count = count;
			IsActive = isActive;
			Date = date;
		}

		#endregion


		public Book Book
		{
			get { return _book; }
			set {
				if (value != null)
				{
					_book = value;
				}
			}
		}

		public long BookId { get; set; }

		public Subscriber Subscriber
		{
			get { return _subscriber; }
			set
			{
				if (value != null)
				{
					_subscriber = value;
				}
			}
		}

		public long SubscriberId { get; set; }

		public int Count { get; set; }

		public bool IsActive { get; set; }

		public DateTime Date { get; set; }
	}
}