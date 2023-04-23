namespace BankApp.Core.Entities
{
    /// <summary>
    /// Will record all changes to account balance
    /// </summary>
    public class Entry
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AccountId { get; set; }

    }
}
