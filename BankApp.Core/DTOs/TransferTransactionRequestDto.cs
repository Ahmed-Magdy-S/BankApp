namespace BankApp.Core.DTOs
{
    public class TransferTransactionRequestDto
    {
        public int FromAccountId { get;set; }
        public int ToAccountId { get;set; }
        public double Amount { get;set; }

    }
}
