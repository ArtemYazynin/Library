using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class EditionMap: EntityTypeConfiguration<Edition>
	{
		public EditionMap()
		{
			Property(x => x.Name).HasMaxLength(300).IsRequired();
			Property(x => x.Year).IsRequired();

			HasMany(x => x.Books).WithRequired(x => x.Edition);
		}
	}
}