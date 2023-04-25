using AutoFixture;
using BankApp.Core.DTOs;
using BankApp.Core.Entities;
using BankApp.Core.RepositoryInterfaces;
using BankApp.Infrastructure.Data;
using BankApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace BankApp.Tests.Repositories
{
    public class MoneyTransactionRepositoryTest
    {
        private readonly IMoneyTransactionRepository _moneyTransactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly Fixture fixture;
        private readonly ITestOutputHelper output;

        public MoneyTransactionRepositoryTest(ITestOutputHelper output)
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseNpgsql("Server=127.0.0.1;Port=5432;Database=bank_app;User Id=postgres;Password=admin;")
             .Options;
            _dbContext = new ApplicationDbContext(dbContextOptions);
            fixture = new();
            _accountRepository = new AccountRepository(_dbContext);
            _moneyTransactionRepository = new MoneyTransactionRepository(_dbContext);
            this.output = output;
        }


        private async Task<Account> CreateRandomAccount()
        {
            //Arrange
            CreateAccountDto createAccountDto = fixture.Create<CreateAccountDto>();

            //Act
            Account? account = await _accountRepository.CreateAccount(createAccountDto);

            //Assert
            Assert.NotNull(account);
            Assert.True(account.OwnerName == createAccountDto.OwnerName);
            Assert.True(account.Balance == createAccountDto.Balance);
            Assert.True(account.Currency == createAccountDto.Currency);
            Assert.True(account.Id > 0);
            Assert.True(account.CreatedAt < DateTime.Now);

            return account;
        }

        [Fact]
        public async Task Transfer_SuccessfullyWithoutDeadlock()
        {
            //Arrange
            var account1 = await CreateRandomAccount();
            var account2 = await CreateRandomAccount();

            output.WriteLine($">>Before: account1: {account1.Balance}, account2: {account2.Balance}");

            int n = 5;//number of transactions
            double amount = 10.0;
            TransferTransactionRequestDto transferData = new() { Amount = amount, FromAccountId = account1.Id, ToAccountId = account2.Id };
            var results = new List<TransferTransactionResultDto>(n);
            //Act
            //Run concurrent transfer transaction
            for (int i = 0; i < n; i++)
            {
                var res = await _moneyTransactionRepository.Transfer(transferData);
                results.Add(res);
            }

            //Assert
            //Check results
            Assert.NotEmpty(results);
            Assert.Equal(results.Count, n);
            for (int i = 0; i < results.Count; i++)
            {
                var result = results[i];

                Assert.NotNull(result);

                //Check Transfer
                var transfer = result.Transfer;
                Assert.True(transfer.FromAccountId == account1.Id);
                Assert.True(transfer.ToAccountId == account2.Id);
                Assert.True(transfer.Amount == amount);
                Assert.True(transfer.Id > 0);
                Assert.True(transfer.CreatedAt < DateTime.Now);

                //Check Entries
                var fromEntry = result.FromEntry;
                Assert.NotNull(fromEntry);
                Assert.True(fromEntry.AccountId == account1.Id);
                Assert.True(fromEntry.Amount == -amount);
                Assert.True(fromEntry.Id > 0);
                Assert.True(fromEntry.CreatedAt < DateTime.Now);

                var toEntry = result.ToEntry;
                Assert.NotNull(toEntry);
                Assert.True(toEntry.AccountId == account2.Id);
                Assert.True(toEntry.Amount == amount);
                Assert.True(toEntry.Id > 0);
                Assert.True(toEntry.CreatedAt < DateTime.Now);


                //Check Accounts
                var fromAccount = result.FromAccount;
                Assert.NotNull(fromAccount);
                Assert.Equal(account1.Id, fromAccount.Id);

                var toAccount = result.ToAccount;
                Assert.NotNull(toAccount);
                Assert.Equal(account2.Id, toAccount.Id);


                //check accounts balances
                output.WriteLine($">>TX No.${i + 1}: account1: {fromAccount.Balance}, account2: {toAccount.Balance}");

                double diff1 = account1.Balance - fromAccount.Balance; //should be 10
                double diff2 = toAccount.Balance - account2.Balance; //should be 10
                Assert.Equal(diff1, diff2);
                output.WriteLine($">>TX diff1: account1: {account1.Balance}, account2: {toAccount.Balance}");

                Assert.True(diff1 > 0.0);
                Assert.True(diff1 % amount == 0.0);

                var k = diff1 / amount;
                Assert.True(k >= 1 && k <= n);
            }




            var updatedAccount1 = await _accountRepository.GetAccountById(account1.Id);
            var updatedAccount2 = await _accountRepository.GetAccountById(account2.Id);

            Assert.NotNull(updatedAccount1);
            Assert.NotNull(updatedAccount2);

            output.WriteLine($">>After: account1: {updatedAccount1.Balance}, account2: {updatedAccount2.Balance}");
            Assert.Equal(account1.Balance - n * amount, updatedAccount1.Balance);
            Assert.Equal(account2.Balance + n * amount, updatedAccount2.Balance);


        }

    }
}
