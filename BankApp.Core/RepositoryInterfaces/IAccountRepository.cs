using BankApp.Core.DTOs;
using BankApp.Core.Entities;

namespace BankApp.Core.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Task<Account?> CreateAccount(CreateAccountDto createAccountDto);
        Task<Account?> GetAccountById(int accountId);
        Task<Account?> GetAccountByIdForUpdate(int accountId);
        Task<IReadOnlyList<Account>> GetAllAccounts(int limit, int offset);
        Task<Account> UpdateAccount(UpdateAccountDto updateAccountDto);
        Task<Account> AddAccountBalance(int accountId, double amount);
        Task DeleteAccount(int accountId);
    }
}
