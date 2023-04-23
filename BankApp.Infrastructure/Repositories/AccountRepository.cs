using BankApp.Core.DTOs;
using BankApp.Core.Entities;
using BankApp.Core.RepositoryInterfaces;

namespace BankApp.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Task<Account> AddAccountBalance(int accountId, double amount)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> CreateAccount(CreateAccountDto createAccountDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> GetAccountById(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> GetAccountByIdForUpdate(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Account>> GetAllAccounts(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<Account> UpdateAccount(UpdateAccountDto updateAccountDto)
        {
            throw new NotImplementedException();
        }
    }
}
