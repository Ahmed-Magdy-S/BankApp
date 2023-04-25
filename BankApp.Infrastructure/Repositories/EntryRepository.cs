using BankApp.Core.Entities;
using BankApp.Core.RepositoryInterfaces;
using BankApp.Infrastructure.Data;

namespace BankApp.Infrastructure.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EntryRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Entry> CreateEntry(int accountId, double amount)
        {
            var entry = new Entry { AccountId = accountId, Amount = amount };
            var result = await _dbContext.Entries.AddAsync(entry);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public Task<IReadOnlyList<Entry>> GetAllEntriesOfAccount(int acountId)
        {
            throw new NotImplementedException();
        }

        public Task<Entry?> GetEntryById(int entryId)
        {
            throw new NotImplementedException();
        }
    }
}
