namespace Library.ObjectModel.Models
{
	public class IncomingBook:Entity
	{
		private Invoice _invoice;
		private Book _book;

		protected IncomingBook()
		{
		}

		public IncomingBook(Invoice invoice, Book book, int count)
		{
			_invoice = invoice;
			_book = book;
			Count = count;
		}

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

		public int Count { get; set; }

		public Invoice Invoice
		{
			get { return _invoice; }
			set {
				if (value!=null)
				{
					_invoice = value;
				}
			}
		}

		public long InvoiceId { get; set; }
	}
}