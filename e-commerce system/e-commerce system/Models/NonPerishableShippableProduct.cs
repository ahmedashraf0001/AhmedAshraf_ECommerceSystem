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
    public class NonPerishableShippableProduct : Product, IShippable // non-expirable & shippable
    {
        public double Weight;
        public NonPerishableShippableProduct(string Name, decimal Price, int Quantity, double Weight) : base(Name, Price, Quantity)
        {
            if (Weight <= 0)
                throw new ArgumentException(Constants.WeightNegative);
            if (Weight > 100000)
                throw new ArgumentException(Constants.WeightExceeded);
            this.Weight = Weight;
        }

        public string GetName() => Name;
        public double GetWeight() => Weight;

        public override bool IsExpired() => false;
        public override bool NeedsShipping() => true;
    }

}
