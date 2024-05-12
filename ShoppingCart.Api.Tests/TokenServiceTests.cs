using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ShoppingCart.Business;
using ShoppingCart.Domain.Requests;
using ShoppingCart.Infrastructure;
using System.Linq.Expressions;

namespace ShoppingCart.Api.Tests
{
    [TestFixture]
    public class TokenServiceTests
    {
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IConfiguration> _mockConfiguration;
        private TokenService _tokenService;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _tokenService = new TokenService(_mockUserRepository.Object, _mockConfiguration.Object);
        }

        [Test]
        public async Task ValidateUser_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var loginModel = new LoginModel { username = "ValidUser" };
            var user = new User { Name = "ValidUser" };
            _mockUserRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);

            // Act
            var result = await _tokenService.ValidateUser(loginModel);

            // Assert
            ClassicAssert.AreEqual(user, result);
        }

        [Test]
        public async Task ValidateUser_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            var loginModel = new LoginModel { username = "InvalidUser" };
            _mockUserRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>())).Returns((User)null);

            // Act
            var result = await _tokenService.ValidateUser(loginModel);

            // Assert
            ClassicAssert.IsNull(result);
        }

        [Test]
        public void GenerateToken_ReturnsToken_WhenUserExists()
        {
            // Arrange
            var user = new User { UserId = 1, Role = "TestRole", Name = "TestUser" };
            _mockConfiguration.Setup(x => x["Salt"]).Returns("TestSalt123456789012345678901234567890");
            _mockConfiguration.Setup(x => x["Issuer"]).Returns("TestIssuer");

            // Act
            var result = _tokenService.GenerateToken(user);

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.IsInstanceOf<string>(result);
        }

        [Test]
        public async Task ValidateUser_ReturnsNull_WhenLoginModelIsNull()
        {
            // Arrange
            LoginModel loginModel = null;

            // Act
            var result = await _tokenService.ValidateUser(loginModel);

            // Assert
            ClassicAssert.IsNull(result);
        }

        [Test]
        public void ValidateUser_ThrowsException_WhenCalled()
        {
            // Arrange
            var loginModel = new LoginModel { username = "ValidUser" };
            _mockUserRepository.Setup(x => x.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>())).Throws(new Exception("Test exception"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _tokenService.ValidateUser(loginModel));
        }

    }

}
