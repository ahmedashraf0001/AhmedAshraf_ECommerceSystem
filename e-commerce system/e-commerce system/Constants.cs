using System;

namespace e_commerce_system
{
    public static class consts
    {
        public const double fee_per_kg = 20.0;

        // product-related messages
        public const string err_missing_name = "Name required";
        public const string err_neg_price = "Price can't be below zero";
        public const string err_neg_qty = "Qty must be positive";
        public const string err_neg_bal = "Balance can't be negative";

        // shipping/weight-related
        public const string err_bad_weight = "Invalid weight";
        public const string err_too_heavy = "Can't ship over 100kg";

        // date validation
        public const string err_expired = "Already expired";
        public const string err_date_past = "Use a future date";
        public const string err_date_too_far = "Max 10 years ahead";

        // cart checks
        public const string err_no_ship_items = "Nothing to ship.\n";
        public const string err_cart_empty = "Cart is empty";
        public const string err_qty_zero = "Qty can't be zero";

        // reductions
        public const string err_neg_amount = "Amount must be positive";
        public const string err_over_stock = "Too many items requested";
        public const string err_over_bal = "Insufficient balance";
    }
}
