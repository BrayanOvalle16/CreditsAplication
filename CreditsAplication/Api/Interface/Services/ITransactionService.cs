using CreditsAplication.Api.Dtos;
using CreditsAplication.Api.Models;

namespace CreditsAplication.Api.Interface.Services
{
    public interface ITransactionService
    {
        Task<Transaction> GetByIdAsync(int id);
        Task<List<Transaction>> GetAllAsync();
        Task<Transaction> AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(int id);
        Task<List<TrasactionReport>> GetTransactionReportAsync(int id, DateTime startDateTime, DateTime endDateTime);
    }
}
