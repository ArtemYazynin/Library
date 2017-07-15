using Library.Web.Controllers.api;
using NUnit.Framework;

namespace Library.Tests.Controllers
{
	[TestFixture]
	public class BooksControllerTest
	{
		[SetUp]
		public void SetUp()
		{
		}

		[Test]
		public void Get()
		{
			var bookController = new BooksController();
		}
	}
}