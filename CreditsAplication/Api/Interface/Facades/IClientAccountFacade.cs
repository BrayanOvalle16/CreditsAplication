using CreditsAplication.Api.Models;

namespace CreditsAplication.Api.Interface.Facades
{
    public interface IClientAccountFacade
    {
        Task<Client> GetByIdAsync(int id);
        Task<List<Client>> GetAllAsync();
        Task<Client> AddAsync(Client account);
        Task UpdateAsync(Client account);
        Task DeleteAsync(int id);
    }
}
