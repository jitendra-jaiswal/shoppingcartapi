using ShoppingCart.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingCart.Domain
{
    public class DiscountDetail
    {
        public string? ProductCode { get; set; }

        public int? CategoryCode { get; set; }

        public int? PercentageDiscount { get; set; }

        public decimal? FixedDiscount { get; set; }

        public decimal? FixedPrice { get; set; }

        public decimal? MaxDiscount { get; set; }

        public string? FreeItem { get; set; }

        public int? MinimumQuantity { get; set; }

        public string? OnItem { get; set; }

        public string? Condition { get; set; }

        public int? LimitCheckout { get; set; }

        public int? LimitforPeriod { get; set; }

        public string? Special { get; set; }
    }
}
