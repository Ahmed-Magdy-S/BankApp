using BankApp.Core.Entities;
using BankApp.Core.RepositoryInterfaces;

namespace BankApp.Infrastructure.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        public Task<Transfer> CreateTransfer(int fromAccountId, int toAccountId, double amount)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Transfer>> GetAllTransfers(int fromAccountId, int toAccountId, int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<Transfer?> GetTransferById(int transferId)
        {
            throw new NotImplementedException();
        }
    }
}
