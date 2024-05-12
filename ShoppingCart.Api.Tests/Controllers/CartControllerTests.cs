using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;
using ShoppingCartApi.Controllers;

namespace ShoppingCart.Api.Tests.Controllers
{
    public class CartControllerTests
    {
        private Mock<ILogger<CartController>> _mockLogger;
        private Mock<IDiscountService> _mockDiscountService;
        private Mock<ICartService> _mockCartService;
        private CartController _cartController;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<CartController>>();
            _mockDiscountService = new Mock<IDiscountService>();
            _mockCartService = new Mock<ICartService>();
            _cartController = new CartController(_mockLogger.Object, _mockDiscountService.Object, _mockCartService.Object);
            var ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { }
            };
            ControllerContext.HttpContext.Items["UserId"] = "1";
            _cartController.ControllerContext = ControllerContext;
        }

        [Test]
        public async Task Get_ReturnsOkResult_WhenCartExists()
        {
            // Arrange
            var userId = 1;
            var cart = new CartModel() { IsSuccess = true };
            _mockCartService.Setup(x => x.GetCart(userId)).Returns(Task.FromResult(cart));

            // Act
            var result = await _cartController.Get();

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task Post_ReturnsBadRequestResult_WhenProductIsNull()
        {
            // Arrange
            var userId = 1;
            var order = new ProductOrder { ProductCode = "InvalidCode", Quantity = 1 };
            _mockCartService.Setup(x => x.GetProductfromDB(order.ProductCode)).Returns(Task.FromResult<Product>(null));

            // Act
            var result = await _cartController.Post(order);

            // Assert
            ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Delete_ReturnsOkResult_WhenProductIsRemoved()
        {
            // Arrange
            var userId = 1;
            var productId = "ValidProductId";
            _mockCartService.Setup(x => x.RemoveProductToCart(userId, productId)).Returns(Task.FromResult(true));

            // Act
            var result = await _cartController.Delete(productId);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task Post_ReturnsOkResult_WhenProductIsAdded()
        {
            // Arrange
            var userId = 1;
            var order = new ProductOrder { ProductCode = "ValidCode", Quantity = 1 };
            var product = new Product { ProductCode = "ValidCode" };
            _mockCartService.Setup(x => x.GetProductfromDB(order.ProductCode)).Returns(Task.FromResult(product));
            _mockCartService.Setup(x => x.UpdateProductInCart(product, userId, order.Quantity)).Returns(Task.FromResult(true));

            // Act
            var result = await _cartController.Post(order);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task Put_ReturnsOkResult_WhenProductIsUpdated()
        {
            // Arrange
            var userId = 1;
            var order = new ProductOrder { ProductCode = "ValidCode", Quantity = 1 };
            var product = new Product { ProductCode = "ValidCode" };
            _mockCartService.Setup(x => x.GetProductfromDB(order.ProductCode)).Returns(Task.FromResult(product));
            _mockCartService.Setup(x => x.UpdateProductInCart(product, userId, order.Quantity)).Returns(Task.FromResult(true));

            // Act
            var result = await _cartController.Put(order);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task Delete_ReturnsOkResult_WhenProductIsDeleted()
        {
            // Arrange
            var userId = 1;
            var productId = "ValidProductId";
            _mockCartService.Setup(x => x.RemoveProductToCart(userId, productId)).ReturnsAsync(true);

            // Act
            var result = await _cartController.Delete(productId);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }
        [Test]
        public async Task Post_ReturnsStatusCode500_WhenExceptionIsThrown()
        {
            // Arrange
            var userId = 1;
            var order = new ProductOrder { ProductCode = "ValidCode", Quantity = 1 };
            var product = new Product { ProductCode = "ValidCode" };
            _mockCartService.Setup(x => x.GetProductfromDB(order.ProductCode)).Returns(Task.FromResult(product));
            _mockCartService.Setup(x => x.UpdateProductInCart(product, userId, order.Quantity)).Throws(new Exception("Test exception"));

            // Act
            var result = await _cartController.Post(order);

            // Assert
            Assert.That(result.Result.GetType() == typeof(ObjectResult));
            var statusCodeResult = result.Result as ObjectResult;
            Assert.That(500 == statusCodeResult.StatusCode);
        }

    }
}