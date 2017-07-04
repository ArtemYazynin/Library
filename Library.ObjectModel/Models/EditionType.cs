using System;
using System.Collections.Generic;
using Library.ObjectModel.Models.Base;

namespace Library.ObjectModel.Models
{
	public class EditionType: Entity
	{
		#region .ctors

		protected EditionType()
		{
		}

		public EditionType(EditionTypeEnum type)
		{
			Type = type;
		}

		#endregion

		private string _name;
		public string Name
		{
			get
			{
				switch (Type)
				{
					case EditionTypeEnum.Electronic:
						return "Электронное";
					case EditionTypeEnum.Printed:
						return "Печатное";
					default:
						throw new Exception(nameof(EditionType));
				}
			}
			protected set { _name = value; }
		}

		public EditionTypeEnum Type { get; set; }

		public virtual ICollection<Edition> Editions { get; set; }
	}
}