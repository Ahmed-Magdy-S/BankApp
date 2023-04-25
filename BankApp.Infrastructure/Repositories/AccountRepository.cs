using BankApp.Core.DTOs;
using BankApp.Core.Entities;
using BankApp.Core.RepositoryInterfaces;
using BankApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Account> AddAccountBalance(int accountId, double amount)
        {
            throw new NotImplementedException();
        }

        public async Task<Account?> CreateAccount(CreateAccountDto createAccountDto)
        {
            var account = new Account
            {
                OwnerName = createAccountDto.OwnerName,
                Balance = createAccountDto.Balance,
                Currency = createAccountDto.Currency,
            };

            var addedAccount = await _dbContext.Accounts.AddAsync(account);
            
            await _dbContext.SaveChangesAsync();

            account = new Account
            {
                OwnerName = addedAccount.Entity.OwnerName,
                Balance = addedAccount.Entity.Balance,
                Currency = addedAccount.Entity.Currency,
                Id = addedAccount.Entity.Id,
                CreatedAt = addedAccount.Entity.CreatedAt
            };

            return account;
        }

        public async Task DeleteAccount(int accountId)
        {
            var account = await GetAccountById(accountId);
            if (account == null) throw new ArgumentException($"The provided account id:{accountId} is not valid");

            _dbContext.Accounts.Remove(account);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Account?> GetAccountById(int accountId)
        {
            return await _dbContext.Accounts.FindAsync(accountId);
        }

        public Task<Account?> GetAccountByIdForUpdate(int accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Account>> GetAllAccounts(int limit, int offset)
        {
            return await _dbContext.Accounts.OrderBy(a => a.Id).Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<Account> UpdateAccount(UpdateAccountDto updateAccountDto)
        {
            var account = await GetAccountById(updateAccountDto.Id);
            if (account == null) throw new ArgumentException($"The provided account id:{updateAccountDto.Id} is not valid");
            account.Balance = updateAccountDto.Balance;
            var result = _dbContext.Accounts.Update(account);
            await _dbContext.SaveChangesAsync();

            return result.Entity;

        }
    }
}
