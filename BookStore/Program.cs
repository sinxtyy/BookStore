using BookStore.DataAccess.Repositories;
using BookStore.DataAccess;
using BookStore.Services;
using BookStore.Security.Jwt;
using BookStore.Security.Hashers;
using Microsoft.EntityFrameworkCore;

namespace BookStore
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			//var conString = "Data Source=DESKTOP-MH5LTPJ\\MSQLSERVER;Initial Catalog=Webb;Integrated Security=True;Trust Server Certificate=True";
			// Add services to the container.
			builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<BookService>();
            builder.Services.AddScoped<TransactionService>();
            builder.Services.AddScoped<UserService>();

            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
			builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

			builder.Services.AddScoped<IBookRepository, BookRepository>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.UseAuthentication();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
