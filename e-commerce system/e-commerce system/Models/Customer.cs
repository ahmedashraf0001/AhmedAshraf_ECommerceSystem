namespace e_commerce_system.Models
{
    public class Customer
    {
        public decimal balance { get; set; }

        public Customer(decimal balance)
        {
            if (balance < 0)
                throw new ArgumentException(consts.err_neg_bal);

            this.balance = balance;
        }

        public decimal takeFromBalance(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException(consts.err_neg_bal);

            if (balance < amount)
                throw new ArgumentException(consts.err_over_bal);

            balance -= amount;
            return balance;
        }
    }
}
