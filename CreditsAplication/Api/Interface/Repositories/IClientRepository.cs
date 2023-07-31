using CreditsAplication.Api.Models;

namespace CreditsAplication.Api.Interface.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetByIdAsync(int id);
        Task<List<Client>> GetAllAsync();
        Task AddAsync(Client cliente);
        Task UpdateAsync(Client cliente);
        Task DeleteAsync(int id);
    }
}
