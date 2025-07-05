using e_commerce_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Products
{

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        private readonly IPerishable _perishableBehavior;
        private readonly IShippable? _shippingBehavior;
        public Product(string Name, decimal Price, int Quantity, IShippable? shippable, IPerishable perishable)
        {
            if(string.IsNullOrEmpty(Name)) throw new ArgumentException(Constants.ProductNameRequired);         
            if(Price < 0) throw new ArgumentException(Constants.PriceNegative);
            if (Quantity < 0) throw new ArgumentException(Constants.QuantityNegative);
            if (Quantity == 0) throw new ArgumentException(Constants.ProductQTYZero);

            this.Name = Name.ToLower().Trim();
            this.Price = Price;
            this.Quantity = Quantity;
            this._shippingBehavior = shippable;
            this._perishableBehavior = perishable;
        }
        public virtual int ReduceQTY(int amount)
        {
            CheckQTY(amount);

            Quantity -= amount;
            return Quantity;
        }
        public virtual void CheckQTY(int amount)
        {
            if (IsExpired())
            {
                throw new ArgumentException(Constants.ProductExpired);
            }

            if (amount == 0) throw new ArgumentException(Constants.ProductQTYZero);
            if (amount < 0) throw new ArgumentException(Constants.InvalidReduceAmount);
            if (amount > Quantity) throw new ArgumentException(Constants.ReduceExceedsStock);
        }
        public virtual bool IsAvailable(int amount)
        {
            try
            {
                CheckQTY(amount);
                return !IsExpired();
            }
            catch (ArgumentException) 
            {         
                return false;
            }
        }
        public bool IsExpired() => _perishableBehavior.IsExpired();
        public bool NeedsShipping()=> _shippingBehavior != null ;
        public double GetWeight() => _shippingBehavior?.GetWeight() ?? 0;
        public IShippable? GetShippingInfo() => _shippingBehavior;

        public override bool Equals(object? obj)
        {
            if (obj is not Product other) return false;
            return Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
        }
        public override int GetHashCode()
        {
            return Name.ToLower().GetHashCode();
        }
    }
}
