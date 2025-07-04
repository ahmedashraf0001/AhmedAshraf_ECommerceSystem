using e_commerce_system.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Models
{
    public class NonPerishableNonShippableProduct : Product // non-expirable & non-shippable
    {
        public NonPerishableNonShippableProduct(string Name, decimal Price, int Quantity) : base(Name, Price, Quantity) { }

        public override bool IsExpired() => false;
        public override bool NeedsShipping() => false;
    }

}
