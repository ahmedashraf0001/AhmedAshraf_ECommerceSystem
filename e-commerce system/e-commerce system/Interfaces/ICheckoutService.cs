using e_commerce_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Interfaces
{
    interface ICheckoutService
    {
        void Checkout(Customer customer, Cart cart);
    }
}
