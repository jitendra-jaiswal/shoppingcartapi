using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ShoppingCart.Business;
using ShoppingCart.Domain;
using ShoppingCart.Infrastructure;
using System.Linq.Expressions;

namespace ShoppingCart.Api.Tests
{
    [TestFixture]
    public class ProductsServiceTests
    {
        private Mock<IRepository<Product>> _mockProductRepository;
        private ProductsService _productsService;

        [SetUp]
        public void Setup()
        {
            _mockProductRepository = new Mock<IRepository<Product>>();
            _productsService = new ProductsService(_mockProductRepository.Object);
        }

        [Test]
        public async Task GetAllProducts_ReturnsProductItems_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product> { new Product { Name = "TestProduct", ProductCode = "TestCode", UnitPrice = 100, Properties = "TestProperties", CategoryNavigation = new ProductCategory { CategoryName = "TestCategory" } } };
            var productItems = new List<ProductItem> { new ProductItem { Name = "TestProduct", ProductCode = "TestCode", UnitPrice = 100, Description = "TestProperties", category = "TestCategory" } };
            _mockProductRepository.Setup(x => x.GetAll(null, It.IsAny<List<Expression<Func<Product, object>>>>())).Returns(products);

            // Act
            var result = await _productsService.GetAllProducts();

            // Assert
            ClassicAssert.AreEqual(productItems.Count, result.Count);
        }


        [Test]
        public async Task GetAllProducts_ReturnsEmptyList_WhenNoProductsExist()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetAll(null, It.IsAny<List<Expression<Func<Product, object>>>>())).Returns(new List<Product>() { new Product { Id = 1, Name = "tets", ProductCode = "T1" } });

            // Act
            var result = await _productsService.GetAllProducts();

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(result.Count, 1);
        }
    }

}
