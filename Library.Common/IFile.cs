namespace Library.Common
{
	public interface IFile<TBook>
	{
		string Name { get; set; }
		string ContentType { get; set; }
		byte[] Content { get; set; }
		TBook Book { get; set; }
	}
}