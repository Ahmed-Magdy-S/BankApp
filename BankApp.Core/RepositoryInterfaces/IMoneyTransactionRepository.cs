using BankApp.Core.DTOs;

namespace BankApp.Core.RepositoryInterfaces
{
    public interface IMoneyTransactionRepository
    {
        Task<TransferTransactionResultDto> Transfer(TransferTransactionRequestDto transferTransactionRequestDto);
    }
}
