using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class PublisherMap: EntityTypeConfiguration<Publisher>
	{
		public PublisherMap()
		{
			Property(x => x.Name).HasMaxLength(1000)
								 .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UX_Name") { IsUnique = true }))
								 .IsRequired();
			HasMany(x => x.Books).WithMany(x => x.Publishers);
		}
	}
}