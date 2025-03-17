using BookStore.Core.Models;
using BookStore.DataAccess;
using BookStore.DataAccess.Repositories;
using BookStore.Security.Hashers;
using BookStore.Security.Jwt;

namespace BookStore.Services
{
    class BookService : BookRepository
    {
        public BookService(BookStoreDbContext dbContext) : base(dbContext) { }

        public async Task AddBook(string title, string description,
            string genre, string autor, string publisher, decimal price)
        {
            await this.AddBookAsync(new Book(title, autor, price, description, publisher, genre));
        }

        public async Task DeleteBook(string title)
        {
            var book = await this.SearchBookAsTitle(title);

            if (book != null)
                await this.DeleteBookAsync(book);
        }

    }
}

