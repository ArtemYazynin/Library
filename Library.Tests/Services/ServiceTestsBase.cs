using Library.ObjectModel.Models;
using Moq;

namespace Library.Tests.Services
{
	abstract class ServiceTestsBase
	{
		protected const string ClrViaCsharpName =
			"CLR via C#. Программирование на платформе Microsoft.NET Framework 4.5 на языке C#";
		protected const string ClrViaCsharpIsbn = "978-5-496-00433-6";
		protected const string ClrViaCsharpPublisherName = "Питер";


		protected Book JsPocketGuide = Mock.Of<Book>(b => b.Id == 2125
		                                                  && b.Name == "JavaScript. Карманный справочник"
		                                                  && b.Isbn == "978-1-449-31685-3"
		                                                  && b.Publisher == Mock.Of<Publisher>());
		protected Book Es6AndNotOnly = Mock.Of<Book>(b => b.Id == 113
		                                                  && b.Name == "ES6 и не только"
		                                                  && b.Isbn == "9781491904244"
		                                                  && b.Publisher == Mock.Of<Publisher>());
		protected Book ClrVia = Mock.Of<Book>(b => b.Id == 10
		                                           && b.Name == ClrViaCsharpName
												   && b.Isbn == ClrViaCsharpIsbn
												   && b.Publisher == Mock.Of<Publisher>(x=>x.Name == ClrViaCsharpPublisherName));
	}
}