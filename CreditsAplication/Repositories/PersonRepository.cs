using CreditsAplication.Api.Exceptions;
using CreditsAplication.Api.Interface.Repositories;
using CreditsAplication.Api.Models;
using CreditsAplication.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CreditsAplication.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AccountApplicationDbContext _dbContext;

        public PersonRepository(AccountApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _dbContext.People.FindAsync(id);
        }

        public async Task<List<Person>> GetAllAsync()
        {
            return await _dbContext.People.ToListAsync();
        }

        public async Task<Person> AddAsync(Person person)
        {
            if(person.Identification != null)
            {
                if(_dbContext.People
                    .Any(x => x.Identification == person.Identification)) 
                {
                    throw new EntityNotFountException($"Identification number {person.Identification} is in used");
                }
            }

            _dbContext.People.Add(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            _dbContext.People.Update(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }

        public async Task DeleteAsync(int id)
        {
            var person = _dbContext.People.Find(id);
            if (person != null)
            {
                _dbContext.People.Remove(person);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
