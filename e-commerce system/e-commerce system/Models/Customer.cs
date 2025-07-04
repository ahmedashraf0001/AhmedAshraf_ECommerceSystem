namespace e_commerce_system.Models
{
    public class Customer
    {
        public decimal Balance { get; set; }
        public Customer(decimal Balance)
        {
            if (Balance < 0) throw new ArgumentException(Constants.BalanceNegative);
            this.Balance = Balance;
        }
        public decimal ReduceBalance(decimal amount)
        {
            if (amount < 0) throw new ArgumentException(Constants.BalanceNegative);
            if (Balance < amount) throw new ArgumentException(Constants.ReduceExceedsBalance);
            Balance -= amount;
            return Balance;
        }
    }
}
