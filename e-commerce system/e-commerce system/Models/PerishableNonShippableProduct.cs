using e_commerce_system.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Models
{
    public class PerishableNonShippableProduct : Product // expirable & non-shippable
    {
        public DateTime ExpirationDate { get; set; }
        public PerishableNonShippableProduct(string Name, decimal Price, int Quantity, DateTime ExpirationDate) : base(Name, Price, Quantity)
        {
            if (ExpirationDate <= DateTime.Today)
                throw new ArgumentException(Constants.InvalidExpiryDate);
            if (ExpirationDate > DateTime.Today.AddYears(10))
                throw new ArgumentException(Constants.ExpiryTooFar);
            this.ExpirationDate = ExpirationDate;
        }
        public override void CheckQTY(int amount)
        {
            if (IsExpired())
            {
                throw new ArgumentException(Constants.ProductExpired);
            }
            base.CheckQTY(amount);
        }
        public override bool IsExpired() => DateTime.Today > ExpirationDate.Date;
        public override bool NeedsShipping() => false;
    }

}
