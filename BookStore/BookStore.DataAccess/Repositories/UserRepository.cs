using BookStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
	public class UserRepository : IUserRepository
	{
		BookStoreDbContext db;

		public UserRepository(BookStoreDbContext dbContext)
		{
			db = dbContext;
		}

		protected async Task AddUserAsync(User user)
		{
			bool a = await this.LoginUser(user);

			if (!a)
			{
				db.Users.Add(user);
				await db.SaveChangesAsync();
			}
		}

		protected async Task DeleteUserAsync(User user)
		{
			if (await this.LoginUser(user))
			{
				db.Users.Remove(user);
				await db.SaveChangesAsync();
			}
		}

		public async Task<string[]> SearchUserAsEmail(string email)
		{
			User? _user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

			if(_user != null)
				return [_user.Login, _user.Email];

			return ["", ""];
		}

		public async Task<string[]> SearchUserAsLogin(string login)
		{
			User? _user = await db.Users.FirstOrDefaultAsync(u => u.Login == login);

			if(_user != null)
				return [_user.Login, _user.Email];

			return ["", ""];
		}

        protected async Task<User?> GetUserAsLogin(string login)
        {
            // User _user = await db.Users.AsNoTracking().SingleAsync(u => u.Login == login);

			User? _user = await db.Users
    			.AsNoTracking()
    			.FirstOrDefaultAsync(u => EF.Functions.Collate(u.Login, "SQL_Latin1_General_CP1_CI_AS") == login);

            return _user;
        }

        protected async Task UpdateUserAsync(User user)
		{
			if (await this.LoginUser(user))
			{
				db.Users.Update(user);
				await db.SaveChangesAsync();
			}
		}

		protected async Task<bool> LoginUser(User user)
		{
			var _user = await db.Users.FirstOrDefaultAsync(u => u.Login == user.Login && u.PasswordHash == user.PasswordHash);

			return _user != null;
		}
	}
}