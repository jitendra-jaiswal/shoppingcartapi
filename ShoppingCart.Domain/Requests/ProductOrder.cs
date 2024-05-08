using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Models
{
    public class ProductOrder
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
