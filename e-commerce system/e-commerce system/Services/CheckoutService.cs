using e_commerce_system.Interfaces;
using e_commerce_system.Models;
using e_commerce_system.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace e_commerce_system.Services
{
    class CheckoutService : ICheckoutService
    {
        public void Checkout(Customer user, Cart cart)
        {
            if (cart.isEmpty())
                throw new ArgumentException(consts.err_cart_empty);

            var original = backup(cart);
            var oldBalance = user.balance;

            try
            {
                var subtotal = calcSubtotal(cart);
                var shippables = getShippingItems(cart);
                var shippingCost = calcShipping(shippables);
                var total = subtotal + (decimal)shippingCost;

                user.takeFromBalance(total);
                applyQtyReduction(cart);

                if (shippables.Any())
                    ShippingService.Ship(shippables);

                print(cart, subtotal, shippingCost, total, user.balance, shippables);
            }
            catch (Exception ex)
            {
                restore(original, user, oldBalance);
                throw new ArgumentException(ex.Message);
            }
        }

        private Dictionary<Product, int> backup(Cart cart)
        {
            return cart.Items.ToDictionary(x => x.Key, x => x.Key.qty);
        }

        private decimal calcSubtotal(Cart cart)
        {
            return cart.Items.Sum(x => x.Key.price * x.Value);
        }

        private List<IShippable> getShippingItems(Cart cart)
        {
            var list = new List<IShippable>();

            foreach (var (p, qty) in cart.Items)
            {
                if (p.needsShipping())
                {
                    var info = p.getShippingDetails();
                    for (int i = 0; i < qty; i++)
                        list.Add(info!);
                }
            }

            return list;
        }

        private double calcShipping(List<IShippable> list)
        {
            return list.Sum(i => i.GetWeight()) * consts.fee_per_kg;
        }

        private void applyQtyReduction(Cart cart)
        {
            foreach (var (p, qty) in cart.Items)
                p.take(qty);
        }

        private void print(Cart cart, decimal subtotal, double shipping, decimal total, decimal balance, List<IShippable> items)
        {
            Console.WriteLine(":: receipt ::");

            foreach (var (p, qty) in cart.Items)
                Console.WriteLine($"{qty}x {p.name,-12} {p.price * qty} EGP");

            Console.WriteLine("--------------------");
            Console.WriteLine($"subtotal:   {subtotal} EGP");
            Console.WriteLine($"shipping:   {shipping:0.00} EGP (at {consts.fee_per_kg} EGP/kg)");
            Console.WriteLine($"total:      {total} EGP");
            Console.WriteLine($"balance:    {balance} EGP\n");
        }

        private void restore(Dictionary<Product, int> originalQtys, Customer user, decimal oldBalance)
        {
            foreach (var (p, qty) in originalQtys)
                p.qty = qty;

            user.balance = oldBalance;
        }
    }
}
