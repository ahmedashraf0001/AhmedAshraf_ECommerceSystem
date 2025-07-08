using e_commerce_system.Interfaces;
using System;

namespace e_commerce_system.Products
{
    public class Product
    {
        public string name { get; set; }
        public decimal price { get; set; }
        public int qty { get; set; }

        private readonly IPerishable _perishable;
        private readonly IShippable? _shippable;

        public Product(string name, decimal price, int qty, IShippable? ship, IPerishable perish)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(consts.err_missing_name);
            if (price < 0) throw new ArgumentException(consts.err_neg_price);
            if (qty < 0) throw new ArgumentException(consts.err_neg_qty);
            if (qty == 0) throw new ArgumentException(consts.err_qty_zero);

            this.name = name.Trim().ToLower();
            this.price = price;
            this.qty = qty;
            _shippable = ship;
            _perishable = perish;
        }

        public int take(int amount)
        {
            check(amount);
            qty -= amount;
            return qty;
        }

        public void check(int amount)
        {
            if (_perishable.IsExpired())
                throw new ArgumentException(consts.err_expired);

            if (amount == 0) throw new ArgumentException(consts.err_qty_zero);
            if (amount < 0) throw new ArgumentException(consts.err_neg_amount);
            if (amount > qty) throw new ArgumentException(consts.err_over_stock);
        }

        public bool canBuy(int amount)
        {
            try
            {
                check(amount);
                return !_perishable.IsExpired();
            }
            catch
            {
                return false;
            }
        }

        public bool needsShipping() => _shippable != null;

        public double getWeight() => _shippable?.GetWeight() ?? 0;

        public IShippable? getShippingDetails() => _shippable;

        public override bool Equals(object? obj)
        {
            if (obj is not Product other) return false;
            return name.Equals(other.name, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return name.ToLower().GetHashCode();
        }
    }
}
