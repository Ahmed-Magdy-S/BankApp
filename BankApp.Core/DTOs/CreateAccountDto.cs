namespace BankApp.Core.DTOs
{
    public class CreateAccountDto
    {
        public required string OwnerName { get; set; }
        public double Balance { get; set; }
        public required string Currency { get; set; }

    }
}
