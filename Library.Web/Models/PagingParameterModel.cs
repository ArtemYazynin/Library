namespace Library.Web.Models
{
	public class PagingParameterModel
	{
		public int Skip { get; set; }
		public int? Take { get; set; }
		public OrderedProperty OrderedProperty { get; set; }
	}
}