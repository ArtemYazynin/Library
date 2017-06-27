using System.Data.Entity.ModelConfiguration;
using System.Security.Cryptography.X509Certificates;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class BookMap: EntityTypeConfiguration<Book>
	{
		public BookMap()
		{
			Property(x => x.Name).HasMaxLength(1000).IsRequired();

			HasRequired(x => x.Edition).WithMany(x => x.Books);

			HasMany(x => x.Authors).WithMany(x => x.Books);
			HasMany(x => x.Publishers).WithMany(x => x.Books);

		}
	}
}
