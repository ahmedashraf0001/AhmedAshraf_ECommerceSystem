using e_commerce_system.Models;
using e_commerce_system.Products;
using e_commerce_system.Services;

namespace e_commerce_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== TEST CASES ==========\n");

            RunTest("Successful Checkout", () =>
            {
                var customer = new Customer(1000m);
                var cart = new Cart();
                var cheese = new PerishableShippableProduct("Cheese", 100m, 5, 0.2, DateTime.Today.AddDays(3));
                var tv = new NonPerishableShippableProduct("TV", 300m, 2, 5.0);
                var scratchCard = new NonPerishableNonShippableProduct("ScratchCard", 50m, 10);
                var biscuits = new PerishableNonShippableProduct("Biscuits", 150m, 3, DateTime.Today.AddDays(2));

                cart.Add(cheese, 2);
                cart.Add(tv, 1);
                cart.Add(scratchCard, 1);
                cart.Add(biscuits, 1);

                var checkout = new CheckoutService();
                checkout.Checkout(customer, cart);
            });

            RunTest("Creating product with expired date", () =>
            {
                var customer = new Customer(1000m);
                var cart = new Cart();
                var expiredCheese = new PerishableShippableProduct("OldCheese", 80m, 3, 0.5, DateTime.Today.AddDays(-1));
                cart.Add(expiredCheese, 1);
                new CheckoutService().Checkout(customer, cart);
            });
            RunTest("Creating product with unrealistic expiring date", () =>
            {
                var customer = new Customer(1000m);
                var cart = new Cart();
                var expiredCheese = new PerishableShippableProduct("UnrealisticCheese", 80m, 3, 0.5, DateTime.Today.AddYears(1000));
                cart.Add(expiredCheese, 1);
                new CheckoutService().Checkout(customer, cart);
            });

            RunTest("Creating product with negative quantity", () =>
            {
                var customer = new Customer(1000m);
                var cart = new Cart();
                var expiredCheese = new PerishableShippableProduct("Cheese", 80m, -5, 0.5, DateTime.Today.AddDays(3));
                cart.Add(expiredCheese, 1);
                new CheckoutService().Checkout(customer, cart);
            });
            RunTest("Creating product with negative price", () =>
            {
                var product = new NonPerishableShippableProduct("Phone", -100m, 3, 1.0);
            });
            RunTest("Creating shippable product with zero weight", () =>
            {
                var product = new NonPerishableShippableProduct("TV", 300m, 1, 0.0);
            });
            RunTest("Creating product with too high weight", () =>
            {
                var product = new NonPerishableShippableProduct("MegaTV", 300m, 1, 1000000);
            });

            RunTest("Creating zero-quantity product", () =>
            {
                var customer = new Customer(1000m);
                var cart = new Cart();
                var item = new NonPerishableShippableProduct("TV", 300m, 0, 5.0);
                cart.Add(item, 1);
            });

            RunTest("Out of Stock Product", () =>
            {
                var customer = new Customer(500m);
                var cart = new Cart();
                var tv = new NonPerishableShippableProduct("TV", 300m, 1, 5.0);
                cart.Add(tv, 2); 
            });
            RunTest("Empty cart checkout", () =>
            {
                var customer = new Customer(1000m);
                var cart = new Cart();
                new CheckoutService().Checkout(customer, cart);
            });

            RunTest("Insufficient Balance", () =>
            {
                var customer = new Customer(100m); 
                var cart = new Cart();
                var tv = new NonPerishableShippableProduct("TV", 300m, 1, 5.0);
                cart.Add(tv, 1);
                new CheckoutService().Checkout(customer, cart);
            });

            RunTest("Invalid Product Name", () =>
            {
                var p = new NonPerishableShippableProduct("", 100m, 2, 1.0); 
            });

            RunTest("Negative Quantity in Cart", () =>
            {
                var cart = new Cart();
                var item = new NonPerishableNonShippableProduct("Item", 50m, 10);
                cart.Add(item, -3);
            });

            Console.WriteLine("========== END OF TESTS ==========\n");
            Console.ReadLine();
        }

        static void RunTest(string description, Action test)
        {
            Console.WriteLine($"--- {description} ---");
            try
            {
                test.Invoke();
                Console.WriteLine("Passed\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: {ex.Message}\n");
            }
        }
    }
}
