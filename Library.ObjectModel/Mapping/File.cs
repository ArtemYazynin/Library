using Library.ObjectModel.Models;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Mapping
{
	public class File : Entity
	{
		public string Name { get; set; }
		public string ContentType { get; set; }
		public byte[] Content { get; set; }

		public virtual Book Book { get; set; }
		public int BookId { get; set; }

	}
}