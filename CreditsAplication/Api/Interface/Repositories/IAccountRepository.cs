using CreditsAplication.Api.Models;

namespace CreditsAplication.Api.Interface.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(int id);
        Task<List<Account>> GetAllAsync();
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(int id);
    }
}
