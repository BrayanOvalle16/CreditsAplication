using CreditsAplication.Api.Interface.Repositories;
using CreditsAplication.Api.Models;
using CreditsAplication.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace CreditsAplication.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AccountApplicationDbContext _dbContext;

        public ClientRepository(AccountApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _dbContext.Clients
                .Include(x => x.Accounts)
                .Include(x => x.Person)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _dbContext.Clients
                .Include(x => x.Accounts)
                .Include(x => x.Person)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Client client)
        {
            _dbContext.Entry(client).Property(x => x.Password).IsModified = false;
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client != null)
            {
                _dbContext.Clients.Remove(client);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}