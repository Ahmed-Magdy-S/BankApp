using BankApp.Core.DTOs;
using BankApp.Core.Entities;
using BankApp.Core.RepositoryInterfaces;
using BankApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Infrastructure.Repositories
{
    public class MoneyTransactionRepository : IMoneyTransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MoneyTransactionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task<Transfer> CreateTransfer(int fromAccountId, int toAccountId, double amount)
        {
            var transfer = new Transfer() { FromAccountId = fromAccountId, ToAccountId = toAccountId, Amount = amount };
            var result = await _dbContext.Transfers.AddAsync(transfer);
            return result.Entity;
        }

        private async Task<Entry> CreateEntry(int accountId, double amount)
        {
            var entry = new Entry { AccountId = accountId, Amount = amount };
            var result = await _dbContext.Entries.AddAsync(entry);
            return result.Entity;
        }

        private async Task<Account> GetAccountById(int accountId)
        {
            return await _dbContext.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == accountId) ?? throw new ArgumentException("The account not found"); ;
        }

        private async Task UpdateAccountBalanceById(int accountId, double amount)
        {
            Account account = await _dbContext.Accounts.FindAsync(accountId) ?? throw new ArgumentException("The account not found");
            account.Balance += amount;
        }

        public async Task<TransferTransactionResultDto> Transfer(TransferTransactionRequestDto request)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                //Create a transfer record between the 2 accounts
                var transfer = await CreateTransfer(request.FromAccountId, request.ToAccountId, request.Amount);

                //Create Entry for each transfered money amount for each account
                var fromEntry = await CreateEntry(request.FromAccountId, -request.Amount);
                var toEntry = await CreateEntry(request.ToAccountId, request.Amount);


                //update balances for both accounts
                await UpdateAccountBalanceById(request.FromAccountId, -request.Amount);
                await UpdateAccountBalanceById(request.ToAccountId, request.Amount);

                await _dbContext.SaveChangesAsync();

                var result = new TransferTransactionResultDto
                {
                    FromEntry = fromEntry,
                    ToEntry = toEntry,
                    Transfer = transfer,
                    FromAccount = await GetAccountById(request.FromAccountId),
                    ToAccount = await GetAccountById(request.ToAccountId)

                };
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }
    }
}
