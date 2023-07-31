using CreditsAplication.Api.Interface.Repositories;
using CreditsAplication.Api.Interface.Services;
using CreditsAplication.Api.Models;

namespace CreditsAplication.Business.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _personRepository.GetByIdAsync(id);
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<Person> AddPersonAsync(Person person)
        {
            return await _personRepository.AddAsync(person);
        }

        public async Task<Person> UpdatePersonAsync(Person person)
        {
            return await _personRepository.UpdateAsync(person);
        }

        public void DeletePerson(int id)
        {
            _personRepository.DeleteAsync(id);
        }
    }

}
