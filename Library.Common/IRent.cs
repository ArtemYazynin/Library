using System;

namespace Library.Common
{
	public interface IRent<TBook, TSubscriber>
	{
		TBook Book { get;  }
		TSubscriber Subscriber { get; }
		int Count { get; }

		bool IsActive { get; }

		DateTime Date { get; }
	}
}