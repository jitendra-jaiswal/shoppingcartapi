using NUnit.Framework;
using NUnit.Framework.Legacy;
using ShoppingCart.Business;
using ShoppingCart.Business.DiscountStrategies;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain;
using System.Text.Json;

namespace ShoppingCart.Api.Tests
{
    [TestFixture]
    public class CacheServiceTests
    {
        private CacheService _cacheService;

        [SetUp]
        public void Setup()
        {
            _cacheService = new CacheService();
        }

        [Test]
        public void GetCacheKey_ReturnsNull_WhenKeyDoesNotExist()
        {
            // Arrange
            var key = "NonExistentKey";

            // Act
            var result = _cacheService.GetCacheKey(key);

            // Assert
            ClassicAssert.IsNull(result);
        }

        [Test]
        public void SetCacheValue_ReturnsTrue_WhenValueIsSet()
        {
            // Arrange
            var key = "TestKey";
            var value = "TestValue";

            // Act
            var result = _cacheService.SetCacheValue(key, value);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void GetDiscountCoupons_ReturnsNull_WhenNoCouponsAreSet()
        {
            // Act
            var result = _cacheService.GetDiscountCoupons();

            // Assert
            ClassicAssert.IsNull(result);
        }

        [Test]
        public void SetDiscountCoupons_ReturnsTrue_WhenCouponsAreSet()
        {
            // Arrange
            var coupons = new List<IDiscountCoupon> { new BOGO_DiscountStrategy(new Domain.DiscountModel() { discountDetail = JsonSerializer.Deserialize<DiscountDetail>("{\"ProductCode\":\"CF1\",\"CategoryCode\":null,\"PercentageDiscount\":null,\"FixedDiscount\":null,\"FixedPrice\":null,\"MaxDiscount\":null,\"FreeItem\":null,\"MinimumQuantity\":null,\"OnItem\":null,\"Condition\":null,\"LimitCheckout\":null,\"LimitforPeriod\":null,\"Special\":\"Buy-One-Get-One Free\"}") }) };

            // Act
            var result = _cacheService.SetDiscountCoupons(coupons);

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void SetCacheValue_ReturnsFalse_WhenKeyIsNull()
        {
            // Arrange
            string key = null;
            var value = "TestValue";

            // Act
            var result = _cacheService.SetCacheValue(key, value);

            // Assert
            Assert.That(result == false);
        }


    }

}
