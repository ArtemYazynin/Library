using System.Collections.Generic;

namespace Library.Common
{
	public interface ISubscriber<TRent>: IPerson
	{
		ICollection<TRent> Rents { get; set; }
	}
}