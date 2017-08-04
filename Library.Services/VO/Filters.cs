namespace Library.Services.VO
{
	public class Filters
	{
		public string ByName { get; set; }
		public string ByAuthor { get; set; }
		public string ByMultipleAuthors { get; set; }
		public string ByAll { get; set; }
		public bool WithoutAuthors { get; set; }
	}
}
