using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
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
			Property(x => x.Isbn).HasMaxLength(50)
								 .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UX_ISBN") { IsUnique = true }))
								 .IsRequired();

			HasRequired(x => x.Edition).WithMany(x => x.Books);

			HasMany(x => x.Authors).WithMany(x => x.Books);
			HasMany(x => x.Publishers).WithMany(x => x.Books);

		}
	}
}
