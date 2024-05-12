using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Domain.Requests;
using ShoppingCart.Infrastructure;
using ShoppingCartApi.Controllers;

namespace ShoppingCart.Api.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<ITokenService> _mockTokenService;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _mockTokenService = new Mock<ITokenService>();
            _userController = new UserController(_mockTokenService.Object);
        }

        [Test]
        public async Task Login_ReturnsOkResult_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginModel { username = "ValidUser", password = "ValidPassword" };
            var user = new User { Name = "ValidUser", Password = "ValidPassword", Role = "User", UserId = 1 };
            _mockTokenService.Setup(x => x.ValidateUser(loginRequest)).Returns(Task.FromResult(user));
            _mockTokenService.Setup(x => x.GenerateToken(user)).Returns("ValidToken");

            // Act
            var result = await _userController.Login(loginRequest);

            // Assert
            ClassicAssert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task Login_ReturnsBadRequestResult_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginRequest = new LoginModel { username = "ValidUser", password = "ValidPassword" };
            _mockTokenService.Setup(x => x.ValidateUser(loginRequest)).Returns(Task.FromResult<User>(null));

            // Act
            var result = await _userController.Login(loginRequest);

            // Assert
            ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task Login_ReturnsStatusCode500_WhenExceptionIsThrown()
        {
            // Arrange
            var loginRequest = new LoginModel { username = "ValidUser", password = "ValidPassword" };
            _mockTokenService.Setup(x => x.ValidateUser(loginRequest)).Throws(new Exception("Test exception"));

            // Act
            var result = await _userController.Login(loginRequest);

            // Assert
            ClassicAssert.IsInstanceOf<ObjectResult>(result.Result);
            var statusCodeResult = result.Result as ObjectResult;
            ClassicAssert.AreEqual(500, statusCodeResult.StatusCode);
        }
    }

}
