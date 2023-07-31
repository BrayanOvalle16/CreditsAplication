using CreditsAplication.Api.Interface.Repositories;
using CreditsAplication.Api.Models;
using CreditsAplication.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CreditsAplication.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountApplicationDbContext _dbContext;

        public AccountRepository(AccountApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _dbContext.Accounts
                .Include(x => x.Transactions)
                .Include(x => x.Client)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Account>> GetAllAsync()
        {
            return await _dbContext.Accounts
                .Include(x => x.Transactions)
                .Include(x => x.Client)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            _dbContext.Accounts.Update(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var account = await _dbContext.Accounts.FindAsync(id);
            if (account != null)
            {
                _dbContext.Accounts.Remove(account);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
