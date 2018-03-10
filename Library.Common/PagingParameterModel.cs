namespace Library.Common
{
	public class PagingParameterModel
	{
		public int Skip { get; set; }
		public int? Take { get; set; }
		public OrderBy OrderBy { get; set; }
		public string Name { get; set; }
	}
}