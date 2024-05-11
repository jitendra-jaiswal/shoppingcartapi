using ShoppingCart.Domain;
using ShoppingCart.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Interfaces
{
    public interface IProductsService
    {
        Task<List<ProductItem>> GetAllProducts();
    }
}
