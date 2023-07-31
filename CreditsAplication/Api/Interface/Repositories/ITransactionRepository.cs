using CreditsAplication.Api.Dtos;
using CreditsAplication.Api.Enums;
using CreditsAplication.Api.Models;

namespace CreditsAplication.Api.Interface.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(int id);
        Task<List<Transaction>> GetAllAsync();
        Task<Transaction> AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(int id);
        Task<decimal> GetTotalBalanceByIdAsync(int accountId, int? transactionId);
        Task<List<Transaction>> GetAllByIdAndDateRangeAsync(int id, DateTime startDateTime, DateTime endDateTime, List<TransactionType> transactionTypes);
        Task<List<TrasactionReport>> GetTransactionReportAsync(int id, DateTime startDateTime, DateTime endDateTime);
    }
}
