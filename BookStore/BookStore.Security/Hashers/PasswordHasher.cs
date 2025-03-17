namespace BookStore.Security.Hashers
{
	class PasswordHasher : IPasswordHasher
	{
		public string GenerateHashPassword(string password) =>
			BCrypt.Net.BCrypt.EnhancedHashPassword(password);

		public bool Verify(string password, string hashedPassword) =>
			BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
	}
}