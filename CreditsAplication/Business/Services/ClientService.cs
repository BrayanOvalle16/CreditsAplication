using CreditsAplication.Api.Interface.Repositories;
using CreditsAplication.Api.Interface.Services;
using CreditsAplication.Api.Models;

namespace CreditsAplication.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clienteRepository;

        public ClientService(IClientRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Task<Client> GetByIdAsync(int id)
        {
            return _clienteRepository.GetByIdAsync(id);
        }

        public Task<List<Client>> GetAllAsync()
        {
            return _clienteRepository.GetAllAsync();
        }

        public Task AddAsync(Client cliente)
        {
            return _clienteRepository.AddAsync(cliente);
        }

        public Task UpdateAsync(Client cliente)
        {
            return _clienteRepository.UpdateAsync(cliente);
        }

        public Task DeleteAsync(int id)
        {
            return _clienteRepository.DeleteAsync(id);
        }
    }
}
