using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCartApi.Controllers;

namespace ShoppingCart.Api.Tests.Controllers
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Mock<IProductsService> _mockProductsService;
        private ProductsController _productsController;

        [SetUp]
        public void Setup()
        {
            _mockProductsService = new Mock<IProductsService>();
            _productsController = new ProductsController(_mockProductsService.Object);
        }

        [Test]
        public async Task Get_ReturnsOkResult_WhenProductsExist()
        {
            // Arrange
            var products = new List<ProductItem> { new ProductItem() };
            _mockProductsService.Setup(x => x.GetAllProducts()).Returns(Task.FromResult(products));

            // Act
            var result = await _productsController.Get();

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task Get_ReturnsStatusCode500_WhenExceptionIsThrown()
        {
            // Arrange
            _mockProductsService.Setup(x => x.GetAllProducts()).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _productsController.Get();

            // Assert
            ClassicAssert.IsInstanceOf<ObjectResult>(result.Result);
            var statusCodeResult = result.Result as ObjectResult;
            ClassicAssert.AreEqual(500, statusCodeResult.StatusCode);
        }



    }

}
