using System.Collections.Generic;

namespace Library.ObjectModel.Models.Base
{
	public interface IBook<TEdition,TPublisher, TGenre, TAuthor, TRent, TInvoice, TFile>
	{
		string Name { get; set; }

		string Isbn { get; set; }

		string Description { get; set; }

		int Count { get; set; }

		int CountAvailable { get; set; }

		TEdition Edition { get; set; }

		TPublisher Publisher { get; set; }
		TFile Cover { get; set; }

		ICollection<TGenre> Genres { get; set; }

		ICollection<TAuthor> Authors { get; set; }
		ICollection<TRent> Rents { get; set; }
		ICollection<TInvoice> Invoices { get; set; }
	}
}