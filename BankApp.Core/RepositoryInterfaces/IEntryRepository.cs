using BankApp.Core.Entities;

namespace BankApp.Core.RepositoryInterfaces
{
    public interface IEntryRepository
    {
        Task<Entry> CreateEntry(int accountId, double amount);
        Task<Entry?> GetEntryById(int entryId);
        Task<IReadOnlyList<Entry>> GetAllEntriesOfAccount(int acountId);
    }
}
