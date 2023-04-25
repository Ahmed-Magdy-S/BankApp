using AutoFixture;
using BankApp.Core.DTOs;
using BankApp.Core.Entities;
using BankApp.Core.RepositoryInterfaces;
using BankApp.Infrastructure.Data;
using BankApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Tests.Repositories
{
    public class AccountRepositoryTest
    {
        private readonly ApplicationDbContext dbContext;
        private readonly Fixture fixture;
        private readonly IAccountRepository _accountRepository;
        public AccountRepositoryTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseNpgsql("Server=127.0.0.1;Port=5432;Database=bank_app;User Id=postgres;Password=admin;")
             .Options;
            dbContext = new ApplicationDbContext(dbContextOptions);
            fixture = new();
            _accountRepository = new AccountRepository(dbContext);
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

        #region CreateAccount
        [Fact]
        public async Task CreateAccount_CreatedCorrectly()
        {
            await CreateRandomAccount();
        }
        #endregion

        #region GetAccountById

        [Fact]
        public async Task GetAccountById_ValidData()
        {
            //Arrange
            var account1 = await CreateRandomAccount();

            //Act
            var account2 = await _accountRepository.GetAccountById(account1.Id);

            //Assert
            Assert.NotNull(account2);
            Assert.True(account1.Id == account2.Id);
            Assert.True(account1.OwnerName == account2.OwnerName);
            Assert.True(account1.Balance == account2.Balance);
            Assert.True(account1.Currency == account2.Currency);
            Assert.True(account1.CreatedAt == account2.CreatedAt);

        }
        #endregion

        #region UpdateAccount
        [Fact]
        public async Task UpdateAccount_UpdatedCorrectly()
        {
            //Arrange
            var account1 = await CreateRandomAccount();
            var updateAccountDto = fixture.Build<UpdateAccountDto>().With(p => p.Id, account1.Id).Create();
            account1.Balance = updateAccountDto.Balance;

            //Act
            var account2 = await _accountRepository.UpdateAccount(updateAccountDto);

            //Assert
            Assert.NotNull(account2);
            Assert.True(account1.Id == account2.Id);
            Assert.True(account1.OwnerName == account2.OwnerName);
            Assert.True(account1.Balance == account2.Balance);
            Assert.True(account1.Currency == account2.Currency);
            Assert.True(account1.CreatedAt == account2.CreatedAt);
        }
        #endregion

        #region DeleteAccount

        [Fact]
        public async Task DeleteAccount_DeletedSuccesssfully()
        {
            //Arrange
            var account1 = await CreateRandomAccount();

            //Act
            await _accountRepository.DeleteAccount(account1.Id);

            //Assert
            var account2 = await _accountRepository.GetAccountById(account1.Id);
            Assert.Null(account2);
        }

        #endregion

        #region GetAllAccounts

        [Fact]
        public async Task GetAllAccounts_GetAccountListAppropriately()
        {
            //Arrange
            for (int i =0; i < 10; i++)
            {
               await CreateRandomAccount();
            }

            //Act
            var accounts = await _accountRepository.GetAllAccounts(offset: 5, limit: 5);

            Assert.NotNull(accounts);
            Assert.NotEmpty(accounts);
            Assert.Equal(5, accounts.Count);
            foreach (var account in accounts)
            {
              Assert.NotNull(account);
            }
        }

        #endregion

    }
}
