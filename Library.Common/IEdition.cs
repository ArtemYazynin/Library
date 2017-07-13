using System.Collections.Generic;

namespace Library.Common
{
	public interface IEdition<TBook>
	{
		string Name { get; set; }
		int Year { get; set; }
		ICollection<TBook> Books { get; set; }
	}
}