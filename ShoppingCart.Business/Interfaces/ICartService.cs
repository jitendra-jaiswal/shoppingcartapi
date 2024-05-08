using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business.Interfaces
{
    public interface ICartService
    {
        Product GetProductfromDB(string productCode);
        bool AddProductToCart(Product product, int userId, int quantity);
        bool UpdateProductInCart(Product product, int userId, int quantity);
        bool RemoveProductToCart(int userId, string productCode);
        CartModel GetCart(int userId);
    }
}