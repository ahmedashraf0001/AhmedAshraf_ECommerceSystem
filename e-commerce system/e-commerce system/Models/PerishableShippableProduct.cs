using e_commerce_system.Interfaces;
using e_commerce_system.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace e_commerce_system.Models
{
    public class PerishableShippableProduct : Product, IShippable //  expirable & shippable  
    {
        public DateTime ExpirationDate { get; set; }
        public double Weight;
        public PerishableShippableProduct(string Name, decimal Price, int Quantity, double Weight, DateTime ExpirationDate) : base(Name, Price, Quantity)
        {
            if (Weight <= 0)
                throw new ArgumentException(Constants.WeightNegative);
            if (Weight > 100000)
                throw new ArgumentException(Constants.WeightExceeded);
            if (ExpirationDate <= DateTime.Today)
                throw new ArgumentException(Constants.InvalidExpiryDate);
            if (ExpirationDate > DateTime.Today.AddYears(10))
                throw new ArgumentException(Constants.ExpiryTooFar);

            this.Weight = Weight;
            this.ExpirationDate = ExpirationDate;
        }

        public string GetName() => Name;
        public double GetWeight() => Weight;

        public override void CheckQTY(int amount)
        {
            if (IsExpired())
            {
                throw new ArgumentException(Constants.ProductExpired);
            }
            base.CheckQTY(amount);
        }
        public override bool IsExpired() => DateTime.Today > ExpirationDate.Date;

        public override bool NeedsShipping() => true;
    }

}
