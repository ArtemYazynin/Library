using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class SubscriberMap: EntityTypeConfiguration<Subscriber>
	{
		public SubscriberMap()
		{
			Property(x => x.Lastname).HasMaxLength(255)
				.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UX_LastnameFirstname") { IsUnique = true, Order = 1 }))
				.IsRequired();
			Property(x => x.Firstname).HasMaxLength(255)
				.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UX_LastnameFirstname") { IsUnique = true, Order = 2 }))
				.IsRequired();
			Property(x => x.Middlename).HasMaxLength(255).IsOptional();
			HasMany(x => x.Rents).WithRequired(x => x.Subscriber);
		}
	}
}