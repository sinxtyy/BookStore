using BookStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess
{
	public class BookStoreDbContext : DbContext
	{
		public DbSet<Book> Books { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

		//public static string connectionString => "Data Source=DESKTOP-MH5LTPJ\\MSQLSERVER;Initial Catalog=Webb;Integrated Security=True;Trust Server Certificate=True";

		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Book>()
				.HasKey(b => b.Id);

			modelBuilder.Entity<User>()
				.HasKey(u => u.Id);

			modelBuilder.Entity<Transaction>()
				.HasKey(t => t.Id);
		}
	}
}