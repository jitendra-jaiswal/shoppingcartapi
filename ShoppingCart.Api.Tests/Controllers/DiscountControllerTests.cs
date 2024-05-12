using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Requests;
using ShoppingCartApi.Controllers;

namespace ShoppingCart.Api.Tests.Controllers
{
    [TestFixture]
    public class DiscountControllerTests
    {
        private Mock<IDiscountService> _mockDiscountService;
        private DiscountController _discountController;

        [SetUp]
        public void Setup()
        {
            _mockDiscountService = new Mock<IDiscountService>();
            _discountController = new DiscountController(_mockDiscountService.Object);
        }

        [Test]
        public async Task Get_ReturnsOkResult_WhenDiscountsExist()
        {
            // Arrange
            var discounts = new List<DiscountModel> { new DiscountModel() };
            _mockDiscountService.Setup(x => x.GetAllActiveDiscounts(false)).Returns(Task.FromResult(discounts.AsEnumerable<DiscountModel>()));

            // Act
            var result = await _discountController.Get();

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetDiscount_ReturnsOkResult_WhenDiscountExists()
        {
            // Arrange
            var discountId = 1;
            var discount = new DiscountModel();
            _mockDiscountService.Setup(x => x.GetDiscounts(discountId)).Returns(Task.FromResult(discount));

            // Act
            var result = await _discountController.Get(discountId);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task Disable_ReturnsOkResult_WhenDiscountIsDisabled()
        {
            // Arrange
            var discountId = 1;

            // Act
            var result = await _discountController.Disable(discountId);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task Delete_ReturnsOkResult_WhenDiscountIsDeleted()
        {
            // Arrange
            var discountId = 1;

            // Act
            var result = await _discountController.Delete(discountId);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task Post_ReturnsOkResult_WhenDiscountIsAdded()
        {
            // Arrange
            var discountModel = new AddDiscountModel();

            // Act
            var result = await _discountController.Post(discountModel);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }
    }

}
