using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class EditionMap: EntityTypeConfiguration<Edition>
	{
		public EditionMap()
		{
			Property(x => x.Name).HasMaxLength(1000).IsRequired();
			Property(x => x.Year).IsRequired();

			HasMany(x => x.EditionTypes).WithMany(x => x.Editions);
			HasMany(x => x.Books).WithRequired(x => x.Edition);
		}
	}
}