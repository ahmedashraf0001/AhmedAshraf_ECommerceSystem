using e_commerce_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Models
{
    class ShippableProduct : IShippable
    {
        public double Weight;
        public string Name;
        public ShippableProduct(string name, double Weight)
        {
            if (Weight <= 0)
                throw new ArgumentException(Constants.WeightNegative);
            if (Weight > 100000)
                throw new ArgumentException(Constants.WeightExceeded);

            this.Weight = Weight;
            this.Name = name;
        }

        public string GetName() => Name;

        public double GetWeight() => Weight;
    }
}
