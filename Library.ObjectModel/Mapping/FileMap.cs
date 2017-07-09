using System.Data.Entity.ModelConfiguration;
using Library.ObjectModel.Models;

namespace Library.ObjectModel.Mapping
{
	public class FileMap : EntityTypeConfiguration<File>
	{
		public FileMap()
		{
			Property(x => x.Name).HasMaxLength(255).IsRequired();
			Property(x => x.ContentType).HasMaxLength(100).IsRequired();
			Property(x => x.Content).HasColumnType("image");

			HasRequired(x => x.Book).WithMany().HasForeignKey(x => x.BookId);
		}
	}
}