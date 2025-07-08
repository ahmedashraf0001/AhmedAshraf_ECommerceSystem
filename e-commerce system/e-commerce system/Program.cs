using e_commerce_system.Models;
using e_commerce_system.Products;
using e_commerce_system.Services;

namespace e_commerce_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== testing chckout scenarios ===\n");

            run("basic checkout should work", () => {
                var user = new Customer(1000m);
                var cart = new Cart();

                var cheese = new Product("Cheese", 100m, 5,
                    new Shippable("Cheese", 0.2),
                    new Perishable(DateTime.Today.AddDays(3)));

                var tv = new Product("TV", 300m, 2,
                    new Shippable("TV", 5),
                    new NonPerishable());

                var scratch = new Product("ScratchCard", 50m, 10, null, new NonPerishable());
                var biscuits = new Product("Biscuits", 150m, 3, null, new Perishable(DateTime.Today.AddDays(2)));

                cart.Add(cheese, 2);
                cart.Add(tv, 1);
                cart.Add(scratch, 1);
                cart.Add(biscuits, 1);

                new CheckoutService().Checkout(user, cart);
            });

            run("expired item should fail", () => {
                var user = new Customer(1000m);
                var cart = new Cart();

                var oldCheese = new Product("OldCheese", 80m, 3,
                    new Shippable("OldCheese", 0.5),
                    new Perishable(DateTime.Today.AddDays(-1)));

                cart.Add(oldCheese, 1);
                new CheckoutService().Checkout(user, cart);
            });

            run("very long expiry date", () => {
                var future = new Product("TimeCapsule", 80m, 3,
                    new Shippable("TimeCapsule", 0.5),
                    new Perishable(DateTime.Today.AddYears(1000)));
            });

            run("negative stock amount", () => {
                var p = new Product("Cheese", 80m, -5,
                    new Shippable("Cheese", 0.5),
                    new Perishable(DateTime.Today.AddDays(3)));
            });

            run("price can't be negative", () => {
                var prod = new Product("Phone", -100m, 3,
                    new Shippable("Phone", 1.0),
                    new NonPerishable());
            });

            run("zero weight item", () => {
                var prod = new Product("TV", 300m, 1,
                    new Shippable("TV", 0),
                    new NonPerishable());
            });

            run("ridiculous weight", () => {
                var bigTV = new Product("MegaTV", 300m, 1,
                    new Shippable("MegaTV", 1_000_000),
                    new NonPerishable());
            });

            run("zero quantity product", () => {
                var item = new Product("TV", 300m, 0,
                    new Shippable("TV", 5),
                    new NonPerishable());
            });

            run("trying to buy too many", () => {
                var buyer = new Customer(500m);
                var cart = new Cart();

                var tv = new Product("TV", 300m, 1,
                    new Shippable("TV", 5),
                    new NonPerishable());

                cart.Add(tv, 2); 
            });

            run("empty cart", () => {
                var buyer = new Customer(1000m);
                var cart = new Cart();
                new CheckoutService().Checkout(buyer, cart);
            });

            run("not enough funds", () => {
                var low = new Customer(100m);
                var cart = new Cart();

                var tv = new Product("TV", 300m, 1,
                    new Shippable("TV", 5),
                    new NonPerishable());

                cart.Add(tv, 1);
                new CheckoutService().Checkout(low, cart);
            });

            run("blank product name", () => {
                var p = new Product("", 100m, 2,
                    new Shippable("Invalid", 1.0),
                    new NonPerishable());
            });

            run("cart negative quantity", () => {
                var cart = new Cart();
                var item = new Product("Item", 50m, 10, null, new NonPerishable());
                cart.Add(item, -3);
            });

            Console.WriteLine("\n=== tests done ===");
            Console.ReadLine();
        }

        static void run(string name, Action test)
        {
            Console.WriteLine($"\n- {name} -");
            try
            {
                test.Invoke();
                Console.WriteLine("passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed: {ex.Message}");
            }
        }
    }
}
