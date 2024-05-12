using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ShoppingCart.Business;
using ShoppingCart.Infrastructure;
using System.Linq.Expressions;

namespace ShoppingCart.Api.Tests
{
    [TestFixture]
    public class CartServiceTests
    {
        private Mock<ILogger<CartService>> _mockLogger;
        private Mock<IRepository<Product>> _mockProductRepository;
        private Mock<IRepository<CartItem>> _mockCartRepository;
        private CartService _cartService;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<CartService>>();
            _mockProductRepository = new Mock<IRepository<Product>>();
            _mockCartRepository = new Mock<IRepository<CartItem>>();
            _cartService = new CartService(_mockLogger.Object, _mockProductRepository.Object, _mockCartRepository.Object);
        }

        [Test]
        public async Task GetProductfromDB_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var productCode = "ValidProductCode";
            var product = new Product { ProductCode = productCode };
            _mockProductRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>())).Returns(product);

            // Act
            var result = await _cartService.GetProductfromDB(productCode);

            // Assert
            Assert.That(product == result);
        }

        [Test]
        public async Task AddProductToCart_ReturnsTrue_WhenProductIsAdded()
        {
            // Arrange
            var product = new Product { ProductCode = "ValidProductCode", UnitPrice = 100 };
            var userId = 1;
            var quantity = 1;
            _mockCartRepository.Setup(x => x.Insert(It.IsAny<CartItem>())).Returns(Task.FromResult(1));

            // Act
            var result = await _cartService.AddProductToCart(product, userId, quantity);

            // Assert
            Assert.That(result);
        }

        [Test]
        public async Task UpdateProductInCart_ReturnsTrue_WhenProductIsUpdated()
        {
            // Arrange
            var product = new Product { ProductCode = "ValidProductCode", UnitPrice = 100 };
            var userId = 1;
            var quantity = 1;
            var cartItem = new CartItem { UserId = userId, ProductId = product.ProductCode, Quantity = quantity };
            _mockCartRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<CartItem, bool>>>())).Returns(cartItem);
            _mockCartRepository.Setup(x => x.Update(It.IsAny<CartItem>())).Returns(Task.FromResult(1));

            // Act
            var result = await _cartService.UpdateProductInCart(product, userId, quantity);

            // Assert
            Assert.That(result);
        }

        [Test]
        public async Task RemoveProductToCart_ReturnsTrue_WhenProductIsRemoved()
        {
            // Arrange
            var userId = 1;
            var productCode = "ValidProductCode";
            var cartItem = new CartItem { UserId = userId, ProductId = productCode };
            _mockCartRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<CartItem, bool>>>())).Returns(cartItem);
            _mockCartRepository.Setup(x => x.Remove(It.IsAny<CartItem>())).Returns(Task.FromResult(1));

            // Act
            var result = await _cartService.RemoveProductToCart(userId, productCode);

            // Assert
            Assert.That(result);
        }

        [Test]
        public async Task GetCart_ReturnsCartModel_WhenCartExists()
        {
            // Arrange
            var userId = 1;
            var cartItems = new List<CartItem> { new CartItem { UserId = userId } };
            _mockCartRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<CartItem, bool>>>(), null)).Returns(cartItems);

            // Act
            var result = await _cartService.GetCart(userId);

            // Assert
            Assert.That(result != null);
            Assert.That(result.IsSuccess);
        }
    }

}
