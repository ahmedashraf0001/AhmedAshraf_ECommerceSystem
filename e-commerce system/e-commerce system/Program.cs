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

                var cheese = new Product("Cheese", 100m, 5,
                    new ShippableProduct("Cheese", 0.2),
                    new PerishableProduct(DateTime.Today.AddDays(3)));

                var tv = new Product("TV", 300m, 2,
                    new ShippableProduct("TV", 5.0),
                    new NonPerishableProduct());

                var scratchCard = new Product("ScratchCard", 50m, 10,
                    null,
                    new NonPerishableProduct());

                var biscuits = new Product("Biscuits", 150m, 3,
                    null,
                    new PerishableProduct(DateTime.Today.AddDays(2)));

                cart.Add(cheese, 2);
                cart.Add(tv, 1);
                cart.Add(scratchCard, 1);
                cart.Add(biscuits, 1);

                new CheckoutService().Checkout(customer, cart);
            });

            RunTest("Creating product with expired date", () =>
            {
                var customer = new Customer(1000m);
                var cart = new Cart();

                var expiredCheese = new Product("OldCheese", 80m, 3,
                    new ShippableProduct("OldCheese", 0.5),
                    new PerishableProduct(DateTime.Today.AddDays(-1)));

                cart.Add(expiredCheese, 1);
                new CheckoutService().Checkout(customer, cart);
            });

            RunTest("Creating product with unrealistic expiring date", () =>
            {
                var unrealistic = new Product("UnrealisticCheese", 80m, 3,
                    new ShippableProduct("UnrealisticCheese", 0.5),
                    new PerishableProduct(DateTime.Today.AddYears(1000)));
            });

            RunTest("Creating product with negative quantity", () =>
            {
                var p = new Product("Cheese", 80m, -5,
                    new ShippableProduct("Cheese", 0.5),
                    new PerishableProduct(DateTime.Today.AddDays(3)));
            });

            RunTest("Creating product with negative price", () =>
            {
                var product = new Product("Phone", -100m, 3,
                    new ShippableProduct("Phone", 1.0),
                    new NonPerishableProduct());
            });

            RunTest("Creating shippable product with zero weight", () =>
            {
                var product = new Product("TV", 300m, 1,
                    new ShippableProduct("TV", 0.0),
                    new NonPerishableProduct());
            });

            RunTest("Creating product with too high weight", () =>
            {
                var product = new Product("MegaTV", 300m, 1,
                    new ShippableProduct("MegaTV", 1000000),
                    new NonPerishableProduct());
            });

            RunTest("Creating zero-quantity product", () =>
            {
                var item = new Product("TV", 300m, 0,
                    new ShippableProduct("TV", 5.0),
                    new NonPerishableProduct());
            });

            RunTest("Out of Stock Product", () =>
            {
                var customer = new Customer(500m);
                var cart = new Cart();

                var tv = new Product("TV", 300m, 1,
                    new ShippableProduct("TV", 5.0),
                    new NonPerishableProduct());

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

                var tv = new Product("TV", 300m, 1,
                    new ShippableProduct("TV", 5.0),
                    new NonPerishableProduct());

                cart.Add(tv, 1);
                new CheckoutService().Checkout(customer, cart);
            });

            RunTest("Invalid Product Name", () =>
            {
                var p = new Product("", 100m, 2,
                    new ShippableProduct("Invalid", 1.0),
                    new NonPerishableProduct());
            });

            RunTest("Negative Quantity in Cart", () =>
            {
                var cart = new Cart();
                var item = new Product("Item", 50m, 10,
                    null,
                    new NonPerishableProduct());

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
