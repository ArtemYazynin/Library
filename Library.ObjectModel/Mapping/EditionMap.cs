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
			Property(x => x.EditionType).IsRequired();

			HasMany(x => x.Books).WithRequired(x => x.Edition);
		}
	}
}