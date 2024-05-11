using ShoppingCart.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Responses
{
    public class ProductsModel : Response
    {
        public List<ProductItem> Products { get; set; }
    }
}
