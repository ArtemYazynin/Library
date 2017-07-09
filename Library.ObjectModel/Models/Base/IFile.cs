namespace Library.ObjectModel.Models.Base
{
	public interface IFile<TBook>
	{
		string Name { get; set; }
		string ContentType { get; set; }
		byte[] Content { get; set; }
		TBook Book { get; set; }
	}
}