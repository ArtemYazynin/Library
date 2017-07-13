using System.Collections.Generic;

namespace Library.Common
{
	public interface IPublisher<TBook>
	{
		string Name { get; set; }
		ICollection<TBook> Books { get; set; }
	}
}