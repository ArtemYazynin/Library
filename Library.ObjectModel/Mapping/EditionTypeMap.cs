using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class EditionTypeMap: EntityTypeConfiguration<EditionType>
	{
		public EditionTypeMap()
		{
			Property(x => x.Name).HasMaxLength(255).IsRequired();
			Property(x => x.Type).IsRequired();
		}
	}
}