using ShoppingCart.Domain;

namespace ShoppingCart.Business.Interfaces
{
    public interface IProductsService
    {
        Task<List<ProductItem>> GetAllProducts();
    }
}
