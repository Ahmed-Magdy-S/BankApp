using BankApp.Core.Entities;
using BankApp.Core.RepositoryInterfaces;

namespace BankApp.Infrastructure.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        public Task<Entry> CreateEntry(int accountId, double amount)
        {
            throw new NotImplementedException();
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
