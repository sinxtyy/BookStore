using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories
{
	interface IUserRepository
	{
		Task<string[]> SearchUserAsLogin(string login);
		Task<string[]> SearchUserAsEmail(string email);
	}
}