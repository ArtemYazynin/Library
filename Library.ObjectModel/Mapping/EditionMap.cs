using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class EditionMap: EntityTypeConfiguration<Edition>
	{
		public EditionMap()
		{
			Property(x => x.Name).HasMaxLength(300)
				.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UX_EditionNameYear") { IsUnique = true, Order = 1 }))
				.IsRequired();
			Property(x => x.Year)
				.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UX_EditionNameYear") { IsUnique = true, Order = 2 }))
				.IsRequired();

			HasMany(x => x.Books).WithRequired(x => x.Edition);
		}
	}
}