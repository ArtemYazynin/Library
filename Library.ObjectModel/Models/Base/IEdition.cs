using System.Collections.Generic;

namespace Library.ObjectModel.Models.Base
{
	public interface IEdition<TBook>
	{
		string Name { get; set; }
		int Year { get; set; }
		ICollection<TBook> Books { get; set; }
	}
}