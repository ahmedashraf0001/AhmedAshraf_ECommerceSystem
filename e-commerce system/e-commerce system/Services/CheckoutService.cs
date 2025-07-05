using e_commerce_system.Interfaces;
using e_commerce_system.Models;
using e_commerce_system.Products;

namespace e_commerce_system.Services
{
    class CheckoutService : ICheckoutService
    {
        public void Checkout(Customer customer, Cart cart)
        {
            if (cart.IsEmpty())
                throw new ArgumentException(Constants.CartPassedEmptly);

            var originalQuantities = SaveOriginalstate(cart);
            var originalBalance = customer.Balance;

            try
            {
                decimal subtotal = CalcSubtotal(cart);
                List<IShippable> items = GetShippables(cart);
                double shippingCost = CalcShipping(items);
                decimal total = subtotal + (decimal)shippingCost;

                customer.ReduceBalance(total);
                ReduceProductQty(cart);

                if (items.Any())
                    ShippingService.Ship(items);

                Print(cart, subtotal, shippingCost, total, customer.Balance, items);
            }
            catch (Exception ex)
            {
                RestoreOriginalState(originalQuantities, customer, originalBalance);
                throw new ArgumentException(ex.Message);
            }
        }

        private Dictionary<Product, int> SaveOriginalstate(Cart cart)
        {
            return cart.Items.ToDictionary(item => item.Key, item => item.Key.Quantity);
        }

        private decimal CalcSubtotal(Cart cart)
        {
            return cart.Items.Sum(item => item.Key.Price * item.Value);
        }

        private List<IShippable> GetShippables(Cart cart)
        {
            List<IShippable> Items = new();

            foreach (var (product, quantity) in cart.Items)
            {
                if (product.NeedsShipping())
                {
                    var Info = product.GetShippingInfo();
                    for (int i = 0; i < quantity; i++)
                        Items.Add(Info!);
                }
            }

            return Items;
        }

        private double CalcShipping(List<IShippable> items)
        {
            return items.Sum(item => item.GetWeight()) * Constants.FeesForKG;
        }

        private void ReduceProductQty(Cart cart)
        {
            foreach (var (product, quantity) in cart.Items)
                product.ReduceQTY(quantity);
        }

        private void Print(Cart cart, decimal subtotal, double shipping, decimal total, decimal balance, List<IShippable> Items)
        {
            Console.WriteLine("** Checkout receipt **");

            foreach (var (product, quantity) in cart.Items)
                Console.WriteLine($"{quantity}x {product.Name,-12} {product.Price * quantity} EGP");

            Console.WriteLine("---------------------------");
            Console.WriteLine($"Subtotal         {subtotal} EGP");
            Console.WriteLine($"Shipping         {shipping} EGP  -> ({Items.Sum(i => i.GetWeight()):0.0} kg × {Constants.FeesForKG} EGP/kg)");
            Console.WriteLine($"Amount           {total} EGP");
            Console.WriteLine($"Balance Left     {balance} EGP\n");
        }

        private void RestoreOriginalState(Dictionary<Product, int> qtys, Customer customer, decimal originalBalance)
        {
            foreach (var (product, quantity) in qtys)
                product.Quantity = quantity;

            customer.Balance = originalBalance;
        }
    }
}
