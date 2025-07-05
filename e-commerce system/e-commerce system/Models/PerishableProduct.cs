using e_commerce_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system.Models
{
    class PerishableProduct : IPerishable
    {
        public DateTime ExpirationDate { get; set; }

        public PerishableProduct(DateTime ExpirationDate)
        {
            if (ExpirationDate <= DateTime.Today)
                throw new ArgumentException(Constants.InvalidExpiryDate);
            if (ExpirationDate > DateTime.Today.AddYears(10))
                throw new ArgumentException(Constants.ExpiryTooFar);

            this.ExpirationDate = ExpirationDate;
        }

        public bool IsExpired() => DateTime.Today > ExpirationDate.Date;
    }
}
