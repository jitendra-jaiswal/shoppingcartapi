using Microsoft.Extensions.Logging;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;

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
        public Product GetProductfromDB(string productCode)
        {
            return _productRepository.GetFirstOrDefault(x => x.ProductCode == productCode);
        }

        public bool AddProductToCart(Product product, int userId, int quantity)
        {
            CartItem item = new CartItem
            {
                UserId = userId,
                ProductId = product.ProductCode,
                UnitPrice = product.UnitPrice.Value,
                Quantity = quantity
            };
            int count = _cartRepository.Insert(item);
            return count == 1;
        }

        public bool UpdateProductInCart(Product product, int userId, int quantity)
        {
            var item = _cartRepository.GetFirstOrDefault(x => x.UserId == userId && x.ProductId == product.ProductCode);
            if (item == null)
            {
                return AddProductToCart(product, userId, quantity);
            };
            item.Quantity = quantity;
            int count = _cartRepository.Update(item);
            return count == 1;
        }

        public bool RemoveProductToCart(int userId, string productCode)
        {
            var cartItem = _cartRepository.GetFirstOrDefault(x => x.UserId == userId && x.ProductId == productCode);
            if (cartItem == null)
            {
                // No Product to delete
                return true;
            }
            int count = _cartRepository.Remove(cartItem);
            return count == 1;
        }

        public CartModel GetCart(int userId)
        {
            CartModel response = new CartModel();

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
            response.TotalAmount = response.CartItems.Sum(x => x.TotalAmountBeforeDiscount);
            response.TotalDiscount = response.CartItems.Sum(x => x.Discount ?? 0);
            response.TotalNetAmount = response.TotalAmount - response.TotalDiscount;
            return response;
        }
    }
}
