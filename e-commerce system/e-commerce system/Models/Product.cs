using e_commerce_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Products
{

    public abstract class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string Name, decimal Price, int Quantity)
        {
            if(string.IsNullOrEmpty(Name)) throw new ArgumentException(Constants.ProductNameRequired);         
            if(Price < 0) throw new ArgumentException(Constants.PriceNegative);
            if (Quantity < 0) throw new ArgumentException(Constants.QuantityNegative);
            if (Quantity == 0) throw new ArgumentException(Constants.ProductQTYZero);

            this.Name = Name.ToLower().Trim();
            this.Price = Price;
            this.Quantity = Quantity;
        }
        public virtual int ReduceQTY(int amount)
        {
            CheckQTY(amount);

            Quantity -= amount;
            return Quantity;
        }
        public virtual void CheckQTY(int amount)
        {
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
        public abstract bool IsExpired();
        public abstract bool NeedsShipping();
    }
}
