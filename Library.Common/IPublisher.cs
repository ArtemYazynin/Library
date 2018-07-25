using System.Collections.Generic;

namespace Library.Common
{
	public interface IPublisher<TBook>
	{
		string Name { get; }
		ICollection<TBook> Books { get; }
	}
}