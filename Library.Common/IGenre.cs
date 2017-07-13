using System.Collections.Generic;

namespace Library.Common
{
	public interface IGenre<TGenre, TBook>
	{
		string Name { get; set; }
		TGenre Parent { get; set; }
		ICollection<TBook> Books { get; set; }
		ICollection<TGenre> Children { get; set; }
	}
}