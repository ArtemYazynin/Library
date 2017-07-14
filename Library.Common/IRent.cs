﻿using System;

namespace Library.Common
{
	public interface IRent<TBook, TSubscriber>
	{
		TBook Book { get; set; }
		TSubscriber Subscriber { get; set; }
		int Count { get; set; }

		bool IsActive { get; set; }

		DateTime Date { get; set; }
	}
}