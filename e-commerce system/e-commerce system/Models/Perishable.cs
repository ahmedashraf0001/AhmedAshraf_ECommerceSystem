using e_commerce_system.Interfaces;
using System;

namespace e_commerce_system.Models
{
    class Perishable : IPerishable
    {
        public DateTime expiration { get; set; }

        public Perishable(DateTime expiration)
        {
            if (expiration <= DateTime.Today)
                throw new ArgumentException(consts.err_date_past);

            if (expiration > DateTime.Today.AddYears(10))
                throw new ArgumentException(consts.err_date_too_far);

            this.expiration = expiration;
        }

        public bool IsExpired()
        {
            return DateTime.Today > expiration.Date;
        }
    }
}
