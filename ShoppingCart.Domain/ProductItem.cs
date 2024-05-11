using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain
{
    public class ProductItem
    {
        public string ProductCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? category { get; set; }

    }
}
