using System.Runtime.Serialization;

namespace Library.Services.DTO
{
	public class IncomingBookDto : EntityDto
	{
		[DataMember]
		public BookDto Book { get; set; }

		[DataMember]
		public int Count { get; set; }

	}
}