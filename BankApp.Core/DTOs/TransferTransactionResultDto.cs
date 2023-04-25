using BankApp.Core.Entities;

namespace BankApp.Core.DTOs
{
    public class TransferTransactionResultDto
    {
        public required Transfer Transfer { get; set; }
        public required Account FromAccount { get; set; }
        public required Account ToAccount { get; set; }
        public required Entry FromEntry { get; set; }
        public required Entry ToEntry { get; set; }

    }
}
