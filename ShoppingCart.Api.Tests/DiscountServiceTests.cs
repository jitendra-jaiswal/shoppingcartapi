using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ShoppingCart.Business;
using ShoppingCart.Business.DiscountStrategies;
using ShoppingCart.Business.Factories;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using ShoppingCart.Domain.Requests;
using ShoppingCart.Domain.Responses;
using ShoppingCart.Infrastructure;
using System.Linq.Expressions;
using System.Text.Json;

namespace ShoppingCart.Api.Tests
{
    [TestFixture]
    public class DiscountServiceTests
    {
        private Mock<ILogger<DiscountService>> _mockLogger;
        private Mock<IRepository<Discount>> _mockDiscountRepository;
        private Mock<ICacheService> _mockCacheService;
        private Mock<IDiscountCouponFactory> _mockCouponFactory;
        private Mock<IRepository<Config>> _mockConfigRepository;
        private Mock<IMapper> _mockMapper;
        private DiscountService _discountService;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<DiscountService>>();
            _mockDiscountRepository = new Mock<IRepository<Discount>>();
            _mockCacheService = new Mock<ICacheService>();
            _mockCouponFactory = new Mock<IDiscountCouponFactory>();
            _mockConfigRepository = new Mock<IRepository<Config>>();
            _mockMapper = new Mock<IMapper>();
            _discountService = new DiscountService(_mockLogger.Object, _mockDiscountRepository.Object, _mockCacheService.Object, _mockCouponFactory.Object, _mockConfigRepository.Object, _mockMapper.Object);
            _mockConfigRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<Config, bool>>>())).Returns(new Config { Id = 1, Key = "DiscountSetDate", Value = DateTime.Now.ToString() });
        }

        [Test]
        public async Task GetDiscounts_ReturnsDiscountModel_WhenDiscountExists()
        {
            // Arrange
            var id = 1;
            var discount = new Discount { Id = id, DetailsJson = "{\"ProductCode\":\"CF1\",\"CategoryCode\":null,\"PercentageDiscount\":null,\"FixedDiscount\":null,\"FixedPrice\":null,\"MaxDiscount\":null,\"FreeItem\":null,\"MinimumQuantity\":null,\"OnItem\":null,\"Condition\":null,\"LimitCheckout\":null,\"LimitforPeriod\":null,\"Special\":\"Buy-One-Get-One Free\"}" };
            var discountModel = new DiscountModel
            {
                Id = id,
                DetailsJson = discount.DetailsJson
            };
            _mockDiscountRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<Discount, bool>>>())).Returns(discount);
            _mockMapper.Setup(x => x.Map<DiscountModel>(discount)).Returns(discountModel);

            // Act
            var result = await _discountService.GetDiscounts(id);

            // Assert
            ClassicAssert.AreEqual(discountModel, result);
        }

        [Test]
        public async Task GetAllActiveDiscounts_ReturnsDiscountModels_WhenDiscountsExist()
        {
            // Arrange
            var discounts = new List<Discount> { new Discount() };
            var discountModels = new List<DiscountModel> { new DiscountModel(){
                Name = "test",
                IsActive = true,
                CreatedDate = DateTime.Now,
                ExpiryDate = DateTime.Now,
                Type = 1,
                DetailsJson = "{\"ProductCode\":\"CF1\",\"CategoryCode\":null,\"PercentageDiscount\":null,\"FixedDiscount\":null,\"FixedPrice\":null,\"MaxDiscount\":null,\"FreeItem\":null,\"MinimumQuantity\":null,\"OnItem\":null,\"Condition\":null,\"LimitCheckout\":null,\"LimitforPeriod\":null,\"Special\":\"Buy-One-Get-One Free\"}",
                discountDetail = JsonSerializer.Deserialize<DiscountDetail>("{\"ProductCode\":\"CF1\",\"CategoryCode\":null,\"PercentageDiscount\":null,\"FixedDiscount\":null,\"FixedPrice\":null,\"MaxDiscount\":null,\"FreeItem\":null,\"MinimumQuantity\":null,\"OnItem\":null,\"Condition\":null,\"LimitCheckout\":null,\"LimitforPeriod\":null,\"Special\":\"Buy-One-Get-One Free\"}")
            } };
            _mockDiscountRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Discount, bool>>>(), It.IsAny<List<Expression<Func<Discount, object>>>>())).Returns(discounts);
            _mockMapper.Setup(x => x.Map<DiscountModel>(It.IsAny<Discount>())).Returns(discountModels.First());

            // Act
            var result = await _discountService.GetAllActiveDiscounts();

            // Assert
            ClassicAssert.AreEqual(discountModels, result);
        }

        [Test]
        public async Task GetAllDiscountCoupons_ReturnsDiscountCoupons_WhenCouponsExist()
        {
            // Arrange
            var discountCoupons = new List<IDiscountCoupon> { new BOGO_DiscountStrategy( new DiscountModel(){
                Name = "test",
                IsActive = true,
                CreatedDate = DateTime.Now,
                ExpiryDate = DateTime.Now,
                Type = 1,
                DetailsJson = "{\"ProductCode\":\"CF1\",\"CategoryCode\":null,\"PercentageDiscount\":null,\"FixedDiscount\":null,\"FixedPrice\":null,\"MaxDiscount\":null,\"FreeItem\":null,\"MinimumQuantity\":null,\"OnItem\":null,\"Condition\":null,\"LimitCheckout\":null,\"LimitforPeriod\":null,\"Special\":\"Buy-One-Get-One Free\"}",
                discountDetail = JsonSerializer.Deserialize<DiscountDetail>("{\"ProductCode\":\"CF1\",\"CategoryCode\":null,\"PercentageDiscount\":null,\"FixedDiscount\":null,\"FixedPrice\":null,\"MaxDiscount\":null,\"FreeItem\":null,\"MinimumQuantity\":null,\"OnItem\":null,\"Condition\":null,\"LimitCheckout\":null,\"LimitforPeriod\":null,\"Special\":\"Buy-One-Get-One Free\"}")
            }) };
            var discounts = new List<Discount> { new Discount() };
            _mockCacheService.Setup(x => x.GetDiscountCoupons()).Returns(discountCoupons);
            _mockCacheService.Setup(x => x.GetCacheKey(It.IsAny<string>())).Returns(DateTime.Now.ToString());

            // Act
            var result = await _discountService.GetAllDiscountCoupons();

            // Assert
            ClassicAssert.AreEqual(discountCoupons, result);
        }

        [Test]
        public async Task ApplyDiscounts_AppliesDiscounts_WhenCartItemsExist()
        {
            // Arrange
            var cart = new CartModel { CartItems = new List<CartItemModel> { new CartItemModel { ProductCode = "CF1", Quantity = 2, UnitPrice = 10, TotalAmountBeforeDiscount = 20 } } };
            var coupons = new List<IDiscountCoupon> { new BOGO_DiscountStrategy(new DiscountModel()
            {
                Name = "test",
                IsActive = true,
                CreatedDate = DateTime.Now,
                ExpiryDate = DateTime.Now,
                Type = 1,
                discountDetail = JsonSerializer.Deserialize<DiscountDetail>("{\"ProductCode\":\"CF1\",\"CategoryCode\":null,\"PercentageDiscount\":null,\"FixedDiscount\":null,\"FixedPrice\":null,\"MaxDiscount\":null,\"FreeItem\":null,\"MinimumQuantity\":null,\"OnItem\":null,\"Condition\":null,\"LimitCheckout\":null,\"LimitforPeriod\":null,\"Special\":\"Buy-One-Get-One Free\"}")
            }) };
            var discountResult = new DiscountResult { ProductCode = "CF1", DiscountAmount = 10 };

            // Act
            await _discountService.ApplyDiscounts(cart, coupons);

            // Assert
            ClassicAssert.AreEqual(discountResult.DiscountAmount, cart.CartItems.First().Discount);
        }

        [Test]
        public void DisableDiscount_ReturnsTrue_WhenDiscountExists()
        {
            // Arrange
            var id = 1;
            var discount = new Discount { Id = id };
            _mockDiscountRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<Discount, bool>>>())).Returns(discount);

            // Act
            var result = _discountService.DisableDiscount(id);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void DeleteDiscount_ReturnsTrue_WhenDiscountExists()
        {
            // Arrange
            var id = 1;
            var discount = new Discount { Id = id };
            _mockDiscountRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<Discount, bool>>>())).Returns(discount);

            // Act
            var result = _discountService.DeleteDiscount(id);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void AddDiscount_ReturnsTrue_WhenDiscountIsAdded()
        {
            // Arrange
            var model = new AddDiscountModel { Name = "TestDiscount", Type = 1, IsActive = true, ExpiryDate = DateTime.Now.AddDays(1), DiscountDetail = JsonSerializer.Deserialize<DiscountDetail>("{\"ProductCode\":\"CF1\",\"CategoryCode\":null,\"PercentageDiscount\":null,\"FixedDiscount\":null,\"FixedPrice\":null,\"MaxDiscount\":null,\"FreeItem\":null,\"MinimumQuantity\":null,\"OnItem\":null,\"Condition\":null,\"LimitCheckout\":null,\"LimitforPeriod\":null,\"Special\":\"Buy-One-Get-One Free\"}") };

            // Act
            var result = _discountService.AddDiscount(model);

            // Assert
            ClassicAssert.IsTrue(result);
        }
        [Test]
        public void AddDiscount_ReturnsFalse_WhenModelIsNull()
        {
            // Arrange
            AddDiscountModel model = null;

            // Act
            var result = _discountService.AddDiscount(model);

            // Assert
            Assert.That(result == false);
        }

        [Test]
        public void GetDiscounts_ThrowsException_WhenCalled()
        {
            // Arrange
            var id = 1;
            _mockDiscountRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<Discount, bool>>>())).Throws(new Exception("Test exception"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _discountService.GetDiscounts(id));
        }

    }

}
