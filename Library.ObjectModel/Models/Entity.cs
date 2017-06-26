using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.ObjectModel.Models
{
	public class Entity
	{
		public Entity()
		{
			Version = DateTime.Now;
		}

		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public DateTime Version { get; set; }
	}
}