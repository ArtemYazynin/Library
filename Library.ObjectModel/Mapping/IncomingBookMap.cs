using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class IncomingBookMap: EntityTypeConfiguration<IncomingBook>
	{
		public IncomingBookMap()
		{
			HasRequired(x => x.Invoice).WithMany(x => x.IncomingBooks);
			Property(x => x.Count).IsRequired();
			HasRequired(x => x.Book).WithMany(x => x.IncomingBooks);
		}
	}
}