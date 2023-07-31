using CreditsAplication.Api.Interface.Repositories;
using CreditsAplication.Api.Interface.Services;
using CreditsAplication.Api.Models;

namespace CreditsAplication.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<Account> GetByIdAsync(int id)
        {
            return _accountRepository.GetByIdAsync(id);
        }

        public Task<List<Account>> GetAllAsync()
        {
            return _accountRepository.GetAllAsync();
        }

        public Task AddAsync(Account account)
        {
            account.AccountNumber = Guid.NewGuid();
            return _accountRepository.AddAsync(account);
        }

        public Task UpdateAsync(Account account)
        {
            return _accountRepository.UpdateAsync(account);
        }

        public Task DeleteAsync(int id)
        {
            return _accountRepository.DeleteAsync(id);
        }
    }
}
