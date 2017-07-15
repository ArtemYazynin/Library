using Library.ObjectModel.Models;
using Moq;

namespace Library.Tests.Services
{
	abstract class ServiceTestsBase
	{
		protected Book JsPocketGuide = Mock.Of<Book>(b => b.Id == 2125
		                                                  && b.Name == "JavaScript. Карманный справочник"
		                                                  && b.Isbn == "978-1-449-31685-3"
		                                                  && b.Publisher == Mock.Of<Publisher>());
		protected Book Es6AndNotOnly = Mock.Of<Book>(b => b.Id == 113
		                                                  && b.Name == "ES6 и не только"
		                                                  && b.Isbn == "9781491904244"
		                                                  && b.Publisher == Mock.Of<Publisher>());
		protected Book ClrVia = Mock.Of<Book>(b => b.Id == 10
		                                           && b.Name == "CLR via C#. Программирование на платформе Microsoft.NET Framework 4.5 на языке C#"
		                                           && b.Isbn == "978-5-496-00433-6"
		                                           && b.Publisher == Mock.Of<Publisher>());
	}
}