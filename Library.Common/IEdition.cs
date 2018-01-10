using System.Collections.Generic;

namespace Library.Common
{
	public interface IEdition<TBook>
	{
		string Name { get; }
		int Year { get; }
		ICollection<TBook> Books { get; }
	}
}