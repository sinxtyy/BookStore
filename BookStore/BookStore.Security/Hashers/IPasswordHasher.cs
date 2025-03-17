namespace BookStore.Security.Hashers
{
	interface IPasswordHasher
	{
		string GenerateHashPassword(string password);
		bool Verify(string password, string hashedPassword);
	}
}