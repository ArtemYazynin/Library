using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class RentMap: EntityTypeConfiguration<Rent>
	{
		public RentMap()
		{
			Property(x => x.Count).IsRequired();
			Property(x => x.IsActive).IsRequired();
			Property(x => x.Date).HasColumnType("datetime2").HasPrecision(0).IsRequired();

			HasRequired(x => x.Book).WithMany(x => x.Rents);
			HasRequired(x => x.Subscriber).WithMany(x => x.Rents);
		}
	}
}