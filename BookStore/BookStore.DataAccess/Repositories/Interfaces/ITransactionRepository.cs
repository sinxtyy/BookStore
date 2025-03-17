using BookStore.Core.Models;

namespace BookStore.DataAccess.Repositories
{
	interface ITransactionRepository
	{
        Task<List<Transaction>> SearchTransactionsAsync(string addresser, string destination);
        Task<List<Transaction>> SearchTransactionsAsAddresser(string addresser);
        Task<Transaction> SearchTransactionAsDate(string addresser, string destination, DateTime date);
        Task<List<Transaction>> SearchTransactionsAsDestination(string destination);
		Task<List<Transaction>> SearchTransactionsAsType(string type);
	}
}