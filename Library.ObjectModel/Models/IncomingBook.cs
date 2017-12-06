namespace Library.ObjectModel.Models
{
	public class IncomingBook:Entity
	{
		public Book Book { get; set; }
		public long BookId { get; set; }

		public int Count { get; set; }

		public Invoice Invoice { get; set; }
		public long InvoiceId { get; set; }
	}
}