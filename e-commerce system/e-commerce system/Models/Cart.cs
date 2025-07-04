using e_commerce_system.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Models
{
    class Cart
    {
        private Dictionary<Product, int> items = new Dictionary<Product, int>();
        public void Add(Product prd, int amount)
        {
            prd.CheckQTY(amount);

            if (items.ContainsKey(prd)) 
            {
                items[prd] += amount;
            }
            else 
            {
                items[prd] = amount;
            }
        }
        public IReadOnlyDictionary<Product, int> Items => items;
        public bool IsEmpty() => items.Count == 0; 
    }
}
