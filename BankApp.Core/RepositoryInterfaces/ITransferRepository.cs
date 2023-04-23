using BankApp.Core.Entities;

namespace BankApp.Core.RepositoryInterfaces
{
    public interface ITransferRepository
    {
        Task<Transfer> CreateTransfer(int fromAccountId, int toAccountId, double amount);
        Task<Transfer?> GetTransferById(int transferId);
        Task<IReadOnlyList<Transfer>> GetAllTransfers(int fromAccountId, int toAccountId, int limit, int offset);
    }
}
