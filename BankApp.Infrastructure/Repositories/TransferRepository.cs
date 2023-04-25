using BankApp.Core.Entities;
using BankApp.Core.RepositoryInterfaces;
using BankApp.Infrastructure.Data;

namespace BankApp.Infrastructure.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransferRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Transfer> CreateTransfer(int fromAccountId, int toAccountId, double amount)
        {
            var transfer = new Transfer() {FromAccountId= fromAccountId, ToAccountId= toAccountId, Amount=amount };
            var result = await _dbContext.Transfers.AddAsync(transfer);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
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
