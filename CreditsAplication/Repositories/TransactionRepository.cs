using CreditsAplication.Api.Dtos;
using CreditsAplication.Api.Enums;
using CreditsAplication.Api.Interface.Repositories;
using CreditsAplication.Api.Models;
using CreditsAplication.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CreditsAplication.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AccountApplicationDbContext _dbContext;

        public TransactionRepository(AccountApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _dbContext.Transactions
                .Include(x => x.Account)
                .ThenInclude(x => x.Client)
                .ThenInclude(x => x.Person)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => id == x.Id);
        }

        public async Task<List<TrasactionReport>> GetTransactionReportAsync(int id, DateTime startDateTime, DateTime endDateTime)
        {
            return await _dbContext.Transactions
                .Include(x => x.Account)
                .ThenInclude(x => x.Client)
                .Where(x => id == x.Account.ClientId)
                .Where(x => x.Date >= startDateTime && x.Date <= endDateTime)
                .Select(x => new TrasactionReport()
                {
                    Date = x.Date,
                    ClientName = x.Account.Client.Person.FullName,
                    AccountNumber = x.Account.AccountNumber,
                    Type = x.Account.AccountType,
                    InicialAmount = x.Account.InitialBalance,
                    State = x.Status,
                    MovementAmount = x.Value
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _dbContext.Transactions
                .Include(x => x.Account)
                .ThenInclude(x => x.Client)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetAllByIdAndDateRangeAsync(int id, DateTime startDateTime, DateTime endDateTime, List<TransactionType> transactionTypes)
        {
            return await _dbContext.Transactions
                .Include(x => x.Account)
                .ThenInclude(x => x.Client)
                .Where(x => x.Date >= startDateTime && x.Date <= endDateTime && x.AccountId == id)
                .Where(x => transactionTypes.Contains(x.TransactionType))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<decimal> GetTotalBalanceByIdAsync(int accountId, int? transactionId)
        {
            var inicialBalance = await GetInicialBalanceByAccountIdAsync(accountId);

            return inicialBalance + await _dbContext.Transactions
                .Where(x => x.Id != transactionId)
                .Where(x => x.AccountId == accountId)
                .Select(x => x.Value)
                .SumAsync();

        }

        protected async Task<decimal> GetInicialBalanceByAccountIdAsync(int accountId)
        {
            return await _dbContext.Accounts
                .Include(x => x.Transactions)
                .Where(x => x.Id == accountId )
                .Select(x => x.InitialBalance)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            _dbContext.Entry(transaction).Property(x => x.TransactionType).IsModified = false;
            _dbContext.Entry(transaction).Property(x => x.AccountId).IsModified = false;
            _dbContext.Entry(transaction).Reference(x => x.Account).IsModified = false;
            _dbContext.Transactions.Update(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _dbContext.Transactions.Remove(transaction);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
