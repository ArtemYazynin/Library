using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class GenreMap: EntityTypeConfiguration<Genre>
	{
		public GenreMap()
		{
			Property(x => x.Name).HasMaxLength(255)
								 .HasColumnAnnotation(IndexAnnotation.AnnotationName, 
													  new IndexAnnotation(new IndexAttribute("UX_GenreName")
																		  {
																			  IsUnique = true
																		  })
													  )
								 .IsRequired();

			HasOptional(x => x.Parent).WithMany(x => x.Children);

			HasMany(x => x.Books).WithMany(x => x.Genres);
		}
	}
}