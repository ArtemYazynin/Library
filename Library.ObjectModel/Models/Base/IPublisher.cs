using System.Collections.Generic;

namespace Library.ObjectModel.Models.Base
{
	public interface IPublisher<TBook>
	{
		string Name { get; set; }
		ICollection<TBook> Books { get; set; }
	}
}