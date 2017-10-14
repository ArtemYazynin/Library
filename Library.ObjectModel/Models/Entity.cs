using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.ObjectModel.Models
{
	public class Entity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		
		[Timestamp]
		public byte[] Version { get; set; }
	}
}