namespace Library.ObjectModel.Models.Base
{
	public interface IPerson
	{
		string Lastname { get; set; }
		string Firstname { get; set; }
		string Middlename { get; set; }
	}
}