using Library.ObjectModel.Models;

namespace Library.DefaultData
{
	public static class Subscribers
	{
		public static Subscriber Ivanov = new Subscriber()
		{
			Lastname = "Иванов",
			Firstname = "Иван",
			Middlename = "Иванович"
		};

		public static Subscriber Petrov = new Subscriber()
		{
			Lastname = "Петров",
			Firstname = "Петр",
			Middlename = "Петрович"
		};

		public static Subscriber Sidorov = new Subscriber()
		{
			Lastname = "Сидоров",
			Firstname = "Матвей",
			Middlename = "Матвеевич"
		};

		public static Subscriber Maslov = new Subscriber()
		{
			Lastname = "Маслов",
			Firstname = "Андрей",
			Middlename = "Евгениевич"
		};
	}
}