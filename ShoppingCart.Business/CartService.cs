using Microsoft.Extensions.Logging;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;
using System.Data;

namespace ShoppingCart.Business
{
    public class CartService : ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<CartItem> _cartRepository;
        public CartService(ILogger<CartService> logger, IRepository<Product> productsRepository, IRepository<CartItem> cartRepository)
        {
            _logger = logger;
            _productRepository = productsRepository;
            _cartRepository = cartRepository;
        }
        public async Task<Product> GetProductfromDB(string productCode)
        {
            return await Task.FromResult(_productRepository.GetFirstOrDefault(x => x.ProductCode == productCode));
        }

        public async Task<bool> AddProductToCart(Product product, int userId, int quantity)
        {
            CartItem item = new CartItem
            {
                UserId = userId,
                ProductId = product.ProductCode,
                UnitPrice = product.UnitPrice.Value,
                Quantity = quantity
            };
            int count = await _cartRepository.Insert(item);
            return count == 1;
        }

        public async Task<bool> UpdateProductInCart(Product product, int userId, int quantity)
        {
            var item = _cartRepository.GetFirstOrDefault(x => x.UserId == userId && x.ProductId == product.ProductCode);
            if (item == null)
            {
                return await AddProductToCart(product, userId, quantity);
            };
            item.Quantity = quantity;
            int count = await _cartRepository.Update(item);
            return count == 1;
        }

        public async Task<bool> RemoveProductToCart(int userId, string productCode)
        {
            var cartItem = _cartRepository.GetFirstOrDefault(x => x.UserId == userId && x.ProductId == productCode);
            if (cartItem == null)
            {
                // No Product to delete
                return true;
            }
            int count = await _cartRepository.Remove(cartItem);
            return count == 1;
        }

        public async Task<CartModel> GetCart(int userId)
        {
            CartModel response = new CartModel() { IsSuccess = true };

            var cartItems = _cartRepository.GetAll(x => x.UserId == userId);
            if (cartItems == null)
            {
                return response;
            }

            response.CartItems = cartItems.Select(x => new Domain.CartItemModel
            {
                ProductCode = x.ProductId,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                TotalAmountBeforeDiscount = x.UnitPrice * x.Quantity
            }).ToList();
            return response;
        }
    }
}
