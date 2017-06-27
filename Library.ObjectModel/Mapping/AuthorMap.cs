using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class AuthorMap :EntityTypeConfiguration<Author>
	{
		public AuthorMap()
		{
			Property(x => x.Lastname).HasMaxLength(255).IsRequired();
			Property(x => x.Firstname).HasMaxLength(255).IsRequired();
			Property(x => x.Middlename).HasMaxLength(255).IsOptional();

			HasMany(x => x.Books).WithMany(x => x.Authors);
		}
	}
}