using System.Collections.Generic;

namespace Library.ObjectModel.Models.Base
{
	public interface IGenre<TGenre, TBook>
	{
		string Name { get; set; }
		TGenre Parent { get; set; }
		ICollection<TBook> Books { get; set; }
		ICollection<TGenre> Children { get; set; }
	}
}