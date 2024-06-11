using System;
using System.Threading.Tasks;
using ChatRoom.Controllers;
using ChatRoom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ChatRoom.Tests
{
    public class AccountControllerTests
    {
        private readonly AccountController _accountController;

        public AccountControllerTests() {
            // Configuração dos mocks
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null, null);
            var mockConfiguration = new Mock<IConfiguration>();
            var mockLogger = new Mock<ILogger<AccountController>>();

            // Inicialização do controller com os mocks
            _accountController = new AccountController(
                mockUserManager.Object, mockSignInManager.Object, mockConfiguration.Object, mockLogger.Object);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken() {
            // Arrange
            var loginModel = new LoginModel { Username = "user1", Password = "Senha123!", RememberMe = false };

            // Act
            var result = await _accountController.Login(loginModel);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.Value);
            Assert.True(okResult.Value.GetType().GetProperty("Token") != null);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsUnauthorized() {
            // Arrange
            var loginModel = new LoginModel { Username = "user1", Password = "SenhaErrada", RememberMe = false };

            // Act
            var result = await _accountController.Login(loginModel);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.NotNull(unauthorizedResult);
            Assert.Equal("Invalid username or password.", unauthorizedResult.Value);
        }
    }
}
