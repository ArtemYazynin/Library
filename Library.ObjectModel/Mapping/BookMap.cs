﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
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
			Property(x => x.Description).HasMaxLength(2000).IsOptional();

			Property(x => x.Count).IsRequired();

			HasRequired(x => x.Edition).WithMany(x => x.Books);
			HasRequired(x => x.Publisher).WithMany(x => x.Books);
			HasOptional(x => x.Cover).WithRequired(x => x.Book);

			HasMany(x => x.Authors).WithMany(x => x.Books);
			HasMany(x => x.Rents).WithRequired(x => x.Book);
			HasMany(x => x.Genres).WithMany(x => x.Books);
		}
	}
}
