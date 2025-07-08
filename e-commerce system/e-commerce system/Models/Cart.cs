using e_commerce_system.Products;
using System.Collections.Generic;

namespace e_commerce_system.Models
{
    class Cart
    {
        private Dictionary<Product, int> items = new();

        public void Add(Product prod, int count)
        {
            if (count < 0)
                throw new ArgumentException(consts.err_neg_amount);

            var current = items.TryGetValue(prod, out var val) ? val : 0;
            var total = current + count;

            prod.check(total); 
            items[prod] = total;
        }

        public IReadOnlyDictionary<Product, int> Items => items;

        public bool isEmpty() => items.Count == 0;
    }
}
