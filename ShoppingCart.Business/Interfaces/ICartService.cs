using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

namespace ShoppingCart.Business.Interfaces
{
    public interface ICartService
    {
        Task<Product> GetProductfromDB(string productCode);
        Task<bool> AddProductToCart(Product product, int userId, int quantity);
        Task<bool> UpdateProductInCart(Product product, int userId, int quantity);
        Task<bool> RemoveProductToCart(int userId, string productCode);
        Task<CartModel> GetCart(int userId);
    }
}