using System.Collections.Generic;

namespace Library.ObjectModel.Models.Base
{
	public interface IAuthor<TBook>: IPerson
	{
		ICollection<TBook> Books { get; set; }
	}
}