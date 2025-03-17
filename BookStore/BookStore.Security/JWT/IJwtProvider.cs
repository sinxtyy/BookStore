using BookStore.Core.Models;

namespace BookStore.Security.Jwt
{
	public interface IJwtProvider
	{
		string GenerateToken(string login);
		Task SetAuthentificationTokenCookie(string token);
		string GetLoginFromCookie();
    }
}