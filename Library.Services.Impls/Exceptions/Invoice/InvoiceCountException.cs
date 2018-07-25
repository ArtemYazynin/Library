using System;

namespace Library.Services.Impls.Exceptions.Invoice
{
	public class InvoiceCountException:Exception
	{
		public InvoiceCountException(string book):base($"After delete invoice, count of book {book} will be negative")
		{
		}
	}
}
