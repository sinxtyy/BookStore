using BookStore.DataAccess.Repositories;
using BookStore.Core.Models;
using BookStore.DataAccess;

namespace BookStore.Services
{
    public class TransactionService : TransactionRepository
    {
        public TransactionService(BookStoreDbContext dbContext) : base(dbContext) { }

        public async Task AddTransaction(string addresser, string destination,
            decimal amount, string type, DateTime date)
        {
            await this.AddTransactionAsync(new Transaction(addresser, destination, amount, type, date));
        }

        //public async Task DeleteTransaction(string addresser, string destination, DateTime date)
        //{
        //    var transaction = await this.SearchTransactionsAsync(addresser, destination);

        //    //if (transaction != null)
        //    //    await this.DeleteTransactionAsync(transaction);
        //}
    }
}
