using CreditsAplication.Api.Models;

namespace CreditsAplication.Api.Interface.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id);
        Task<List<Person>> GetAllAsync();
        Task<Person> AddAsync(Person person);
        Task<Person> UpdateAsync(Person person);
        Task DeleteAsync(int id);
    }
}
