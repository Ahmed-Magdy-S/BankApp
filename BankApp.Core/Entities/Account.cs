namespace BankApp.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string? OwnerName { get; set; }
        public double Balance { get; set;}
        public string? Currency { get; set; }
        public DateTime CreatedAt { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType != this.GetType) return false;
            Account other = (Account) obj;
            if (
                other.Id != Id ||
                other.OwnerName !=OwnerName ||
                !other.Balance.Equals(Balance) ||
                other.Currency != Currency ||
                other.CreatedAt != CreatedAt
               )
                return false;
            return true;
            

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id,OwnerName,CreatedAt);
        }
    }
}
