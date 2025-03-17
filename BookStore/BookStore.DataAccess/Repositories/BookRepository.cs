using BookStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
	public class BookRepository : IBookRepository
	{
		//private readonly DbContextOptionsBuilder<BookStoreDbContext> optionsBuilder = new DbContextOptionsBuilder<BookStoreDbContext>();            
		BookStoreDbContext db;

		public BookRepository(BookStoreDbContext dbContext)
		{
			//optionsBuilder.UseSqlServer(BookStoreDbContext.connectionString);

			db = dbContext;
		}

		protected async Task AddBookAsync(Book book)
		{
			if (this.SearchBookAsTitle(book.Title) == null)
			{
				db.Books.Add(book);
				await db.SaveChangesAsync();
			}
		}

		protected async Task DeleteBookAsync(Book book)
		{
			if (this.SearchBookAsTitle(book.Title) != null)
			{
				db.Books.Remove(book);
				await db.SaveChangesAsync();
			}
		}

		public async Task<Book?> SearchBookAsTitle(string title)
		{
			Book? _book = await db.Books.FirstOrDefaultAsync(b => b.Title == title);

			return _book;
		}

		public async Task<Book?> SearchBookAsAutor(string autor)
		{
			Book? _book = await db.Books.FirstOrDefaultAsync(b => b.Autor == autor);

			return _book;
		}

		public async Task<Book?> SearchBookAsGenre(string genre)
		{
			Book? _book = await db.Books.FirstOrDefaultAsync(b => b.Genre == genre);

			return _book;
		}

		protected async Task UpdateBookAsync(Book book)
		{
			if (this.SearchBookAsTitle(book.Title) != null)
			{
				db.Books.Update(book);
				await db.SaveChangesAsync();
			}
		}
	}
}