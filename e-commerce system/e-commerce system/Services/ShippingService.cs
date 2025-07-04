using e_commerce_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Services
{
    public static class ShippingService
    {
        public static void Ship(List<IShippable> items)
        {
            Console.WriteLine("** Shipment notice **");

            if (items.Count == 0) throw new ArgumentException(Constants.NoShippingItems);

            double totalweight = 0;
            var model = items.GroupBy(e => e.GetName()).ToDictionary(e => e.Key, g => g.ToList());
            foreach(var group in model)
            {
                double groupWeight = group.Value.Sum(e => e.GetWeight());
                totalweight += groupWeight;
                Console.WriteLine($"{group.Value.Count}x {group.Key, -12} {groupWeight * 1000}g");
            }
            Console.WriteLine($"Total package weight {totalweight:F1}kg\n");
        }
    }
}
