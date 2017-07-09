using Library.ObjectModel.Models.Base;

namespace Library.Services.DTO
{
	public class FileDto:EntityDto, IFile<BookDto>
	{
		public string Name { get; set; }
		public string ContentType { get; set; }
		public byte[] Content { get; set; }
		public BookDto Book { get; set; }
	}
}