using System.Collections.Generic;
using Library.Common;

namespace Library.ObjectModel.Models
{
	public class Subscriber:Entity, ISubscriber<Rent>, IDeletable
	{
		private string _lastname;
		private string _firstname;
		private ICollection<Rent> _rents;

		protected Subscriber()
		{
			_rents = new List<Rent>();
		}

		public Subscriber(string lastname, string firstname)
		{
			_lastname = lastname;
			_firstname = firstname;
			_rents = new List<Rent>();
		}

		public string Lastname
		{
			get { return _lastname; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_lastname = value;
				}
			}
		}

		public string Firstname
		{
			get { return _firstname; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					_firstname = value;
				}
			}
		}

		public string Middlename { get; set; }

		public string Fio => $"{Lastname} {Firstname} {Middlename ?? string.Empty}";

		public ICollection<Rent> Rents
		{
			get { return _rents; }
			set
			{
				if (value == null)
				{
					_rents.Clear();
				}
				else
				{
					foreach (var rent in value)
					{
						if (rent!=null)
						{
							_rents.Add(rent);
						}
					}
				}
			}
		}

		public bool IsDeleted { get; set; }
	}
}