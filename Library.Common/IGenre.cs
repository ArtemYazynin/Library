using System.Collections.Generic;

namespace Library.Common
{
	public interface IGenre<TGenre, TBook>
	{
		string Name { get; }
		TGenre Parent { get; }
		ICollection<TBook> Books { get; }
		ICollection<TGenre> Children { get; }
	}
}