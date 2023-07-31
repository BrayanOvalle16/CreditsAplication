using CreditsAplication.Api.Models;

namespace CreditsAplication.Api.Interface.Services
{
    public interface IPersonService
    {
        Task<Person> GetPersonByIdAsync(int id);
        Task<List<Person>> GetAllPeopleAsync();
        Task<Person> AddPersonAsync(Person person);
        Task<Person> UpdatePersonAsync(Person person);
        void DeletePerson(int id);
    }

}
