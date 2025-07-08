using e_commerce_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Models
{
    class Shippable : IShippable
    {
        public double weight;
        public string name;
        public Shippable(string name, double Weight)
        {
            if (Weight <= 0)
                throw new ArgumentException(consts.err_bad_weight);
            if (Weight > 100000)
                throw new ArgumentException(consts.err_too_heavy);

            this.weight = Weight;
            this.name = name;
        }

        public string GetName() => name;

        public double GetWeight() => weight;
    }
}
