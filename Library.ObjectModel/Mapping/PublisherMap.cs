using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class PublisherMap: EntityTypeConfiguration<Publisher>
	{
		public PublisherMap()
		{
			Property(x => x.Name).HasMaxLength(1000).IsRequired();
			HasMany(x => x.Books).WithMany(x => x.Publishers);
		}
	}
}