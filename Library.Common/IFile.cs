namespace Library.Common
{
	public interface IFile<TBook>
	{
		string Name { get; }
		string ContentType { get; }
		byte[] Content { get; }
		TBook Book { get; }
	}
}