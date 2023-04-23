namespace BankApp.Core.Entities
{
    /// <summary>
    ///will record all money transfers between 2 accounts
    /// </summary>
    public class Transfer
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
