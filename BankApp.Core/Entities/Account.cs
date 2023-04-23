namespace BankApp.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string? OwnerName { get; set; }
        public double Balance { get; set;}
        public string? Currency { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
