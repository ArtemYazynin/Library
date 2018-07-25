using System.Collections.Generic;

namespace Library.Common
{
	public interface IBook<TEdition,TPublisher, TGenre, TAuthor, TRent, TInvoice, TFile>
	{
		string Name { get; }

		string Isbn { get; }

		string Description { get; }

		int Count { get; }

		TEdition Edition { get; }

		TPublisher Publisher { get; }
		TFile Cover { get; }

		ICollection<TGenre> Genres { get; }

		ICollection<TAuthor> Authors { get; }
		ICollection<TRent> Rents { get; }
		ICollection<TInvoice> IncomingBooks { get; }
	}
}