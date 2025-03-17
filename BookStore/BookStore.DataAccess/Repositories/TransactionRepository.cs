using BookStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        //private readonly DbContextOptionsBuilder<BookStoreDbContext> optionsBuilder = new DbContextOptionsBuilder<BookStoreDbContext>();            
        BookStoreDbContext db;

        public TransactionRepository(BookStoreDbContext dbContext)
        {
            //optionsBuilder.UseSqlServer(BookStoreDbContext.connectionString);

            db = dbContext;
        }

        protected async Task AddTransactionAsync(Transaction transaction)
        {
            if (this.SearchTransactionsAsAddresser(transaction.Addresser) == null)
            {
                db.Transactions.Add(transaction);
                await db.SaveChangesAsync();
            }
        }

        protected async Task DeleteTransactionAsync(Transaction transaction)
        {
            if (this.SearchTransactionsAsAddresser(transaction.Addresser) != null)
            {
                db.Transactions.Remove(transaction);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Transaction>> SearchTransactionsAsync(string addresser, string destination)
        {
            var transaction = await db.Transactions
                .Where(t => t.Addresser == addresser && t.Destination == destination)
                .ToListAsync();

            return transaction.ToList();
        }

        public async Task<List<Transaction>> SearchTransactionsAsAddresser(string addresser)
        {
            var transaction = await db.Transactions
                .Where(t => t.Addresser == addresser)
                .ToListAsync();

            return transaction;
        }

        public async Task<Transaction> SearchTransactionAsDate(string addresser, string destination, DateTime date)
        {
            var transaction = await db.Transactions
                .Where(t => t.Addresser == addresser && t.Destination == destination)
                .Where(t => t.Date >= date.AddSeconds(-30) && t.Date <= date.AddSeconds(30))
                .ToListAsync();

            return transaction.FirstOrDefault();
        }

        public async Task<List<Transaction>> SearchTransactionsAsDestination(string destination)
        {
            var transaction = await db.Transactions
                .Where(t => t.Destination == destination)
                .ToListAsync();

            return transaction;
        }

        public async Task<List<Transaction>> SearchTransactionsAsType(string type)
        {
            var transaction = await db.Transactions
                .Where(t => t.Type == type)
                .ToListAsync();

            return transaction;
        }
    }
}