using e_commerce_system.Interfaces;
using e_commerce_system.Models;


namespace e_commerce_system.Services
{
    class CheckoutService: ICheckoutService
    {   
        public void Checkout(Customer customer, Cart cart) 
        {
            if (cart.IsEmpty()) throw new ArgumentException(Constants.CartPassedEmptly);

            decimal subtotal = 0;
            decimal total = 0;
            double shippingfees = 0;

            List<IShippable> shippables = new();
            var originalQty = cart.Items.ToDictionary(p => p.Key, p => p.Key.Quantity);
            var originalBalance = customer.Balance;

            try
            {
                foreach (var item in cart.Items)
                {
                    item.Key.ReduceQTY(item.Value);

                    decimal itemtotal = item.Value * item.Key.Price;
                    subtotal += itemtotal;

                    if (item.Key.NeedsShipping())
                    {
                        var shipInfo = item.Key.GetShippingInfo();
                        for (int i = 0; i < item.Value; i++)
                            shippables.Add(shipInfo!);
                    }
                }
                shippingfees = shippables.Sum(e => e.GetWeight()) * Constants.FeesForKG;
                total = (decimal)shippingfees + subtotal;

                customer.ReduceBalance(total);

                if (shippables.Any())
                    ShippingService.Ship(shippables);
            
                Print(cart, subtotal, shippingfees, total ,customer.Balance, shippables);
            }
            catch(Exception ex) 
            {
                foreach (var kv in originalQty)
                {
                    kv.Key.Quantity = kv.Value;
                }
                customer.Balance = originalBalance;

                throw new ArgumentException($"{ex.Message}");
            }
        }
        private void Print(Cart cart, decimal subtotal, double shippingfees, decimal total, decimal balance, List<IShippable> shippables)
        {
            Console.WriteLine("** Checkout receipt **");

            foreach (var entry in cart.Items)
                Console.WriteLine($"{entry.Value}x {entry.Key.Name,-12} {entry.Key.Price * entry.Value} EGP");

            Console.WriteLine("---------------------------");
            Console.WriteLine($"Subtotal         {subtotal} EGP");
            Console.WriteLine($"Shipping         {shippingfees} EGP  -> ({shippables.Sum(e => e.GetWeight()):0.0} kg × {Constants.FeesForKG} EGP/kg)");
            Console.WriteLine($"Amount           {total} EGP");
            Console.WriteLine($"Balance Left     {balance} EGP\n");
        }

    }
}
