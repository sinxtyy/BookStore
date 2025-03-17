using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Models
{
	public class User
	{
		public User()	{ }

		public User(string login, string passwordHash, string email)
		{
			Login = login;
			PasswordHash = passwordHash;
			Email = email;
		}

		[Key]
		public Guid Id { get; }

		public string Login { get; set; } = string.Empty;

		public string PasswordHash { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;
	}
}
