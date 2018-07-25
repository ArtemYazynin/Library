using Library.Common;

namespace Library.ObjectModel.Models
{
	public class File : Entity, IFile<Book>
	{
		public string Name { get; set; }
		public string ContentType { get; set; }
		public byte[] Content { get; set; }

		public Book Book { get; set; }
		public int BookId { get; set; }
	}
}