using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class InvoiceMap: EntityTypeConfiguration<Invoice>
	{
		public InvoiceMap()
		{
			Property(x => x.Date).HasColumnType("datetime2").HasPrecision(0).IsRequired();
			HasMany(x => x.Books).WithMany(x => x.Invoices);
		}
	}
}