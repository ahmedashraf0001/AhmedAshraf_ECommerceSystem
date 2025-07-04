using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_system
{
    public static class Constants
    {
        public const double FeesForKG = 20.0;

        public const string ProductNameRequired = "Product name can't be empty";
        public const string PriceNegative = "Price can't be negative";
        public const string QuantityNegative = "Quantity can't be negative";
        public const string BalanceNegative = "Balance can't be negative";

        public const string WeightNegative = "Weight must be greater than zero";
        public const string WeightExceeded = "Weight can't excceed 100kg";
        public const string InvalidExpiryDate = "Expiration date must be in the future when adding new product";
        public const string ExpiryTooFar = "Expiration date can't be more than 10 years in the future";
        public const string ProductExpired = "Product is expired";

        public const string InvalidReduceAmount = "The amount can't be negative";
        public const string ReduceExceedsStock = "Amount exceeds the available quantity";
        public const string ReduceExceedsBalance = "Amount exceeds the available balance";
        public const string NoShippingItems = "No shippable items found.\n";
        public const string CartPassedEmptly = "Cart is empty";
        public const string ProductQTYZero = "Product Qty can't be zero";

    }
}
