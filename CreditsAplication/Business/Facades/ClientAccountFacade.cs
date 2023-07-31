using CreditsAplication.Api.Exceptions;
using CreditsAplication.Api.Interface.Facades;
using CreditsAplication.Api.Interface.Helpers;
using CreditsAplication.Api.Interface.Services;
using CreditsAplication.Api.Models;

namespace CreditsAplication.Business.Facades
{
    public class ClientAccountFacade : IClientAccountFacade
    {
        private IPersonService _personService;
        private IClientService _cliService;
        private IPasswordHasherHelper _passwordHasherHelper;

        public ClientAccountFacade(IPersonService personService, IClientService cliService, IPasswordHasherHelper passwordHasherHelper)
        {
            _personService = personService;
            _cliService = cliService;
            _passwordHasherHelper = passwordHasherHelper;
        }

        public async Task<Client> AddAsync(Client client)
        {
            if(client.Person != null)
            {
                await _personService.AddPersonAsync(client.Person);
                client.PersonId = client.Person.Id;
            }
            client.Password = _passwordHasherHelper.Hash(client.Password);
            client.Person = null;
            
            await _cliService.AddAsync(client);
            return client;
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _cliService.GetByIdAsync(id);
            if(client != null)
            {
                if(client.PersonId != null)
                {
                    _personService.DeletePerson(client.PersonId.Value);
                }
                await _cliService.DeleteAsync(id);
            } else
            {
                throw new EntityNotFountException($"Client with id {id} not found");
            }
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _cliService.GetAllAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _cliService.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Client client)
        {
            if (client.Person != null)
            {
                await _personService.UpdatePersonAsync(client.Person);
            }
            await _cliService.UpdateAsync(client);
        }
    }
}
