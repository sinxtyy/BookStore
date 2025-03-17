using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories
{
	interface IBookRepository
	{
		Task<Book?> SearchBookAsTitle(string title);
		Task<Book?> SearchBookAsAutor(string autor);
		Task<Book?> SearchBookAsGenre(string genre);
	}
}