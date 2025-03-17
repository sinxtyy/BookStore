using BookStore.DataAccess.Repositories;
using BookStore.Security.Hashers;
using BookStore.Security.Jwt;
using BookStore.Core.Models;
using BookStore.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Services
{
    public class UserService : UserRepository
    {
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtProvider _jwtProvider;

        public UserService(BookStoreDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _passwordHasher = new PasswordHasher();
            _jwtProvider = new JwtProvider(httpContextAccessor);
        }

        public async Task<bool> Register(string login, string password, string email)
        {
            var hashedPassword = _passwordHasher.GenerateHashPassword(password);

            if (await this.GetUserAsLogin(login) == null)
            {
                await this.AddUserAsync(new User(login, hashedPassword, email));

                await _jwtProvider.SetAuthentificationTokenCookie(_jwtProvider.GenerateToken(login));

                return true;
            }

            return false;
        }

        public async Task<bool> Login(string login, string password)
        {
            var user = await this.GetUserAsLogin(login);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                return false;
            }

            await _jwtProvider.SetAuthentificationTokenCookie(_jwtProvider.GenerateToken(login));

            return true;
        }
    }
}
