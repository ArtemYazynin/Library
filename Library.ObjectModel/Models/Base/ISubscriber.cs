using System.Collections.Generic;

namespace Library.ObjectModel.Models.Base
{
	public interface ISubscriber<TRent>: IPerson
	{
		ICollection<TRent> Rents { get; set; }
	}
}