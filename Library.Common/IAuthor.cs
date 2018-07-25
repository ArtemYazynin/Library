using System.Collections.Generic;

namespace Library.Common
{
	public interface IAuthor<TBook>: IPerson
	{
		ICollection<TBook> Books { get; }
	}
}