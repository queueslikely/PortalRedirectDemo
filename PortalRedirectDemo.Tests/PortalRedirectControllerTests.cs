global using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using PortalRedirectDemo.Controllers;
using PortalRedirectDemo.Models;
using System.Security.Claims;

namespace PortalRedirectDemo.Tests
{
    public class PortalRedirectControllerTests
    {
        private RedirectSettings redirectSettings;

        public PortalRedirectControllerTests()
        {
            // Create a mock RedirectSettings class
            redirectSettings = new RedirectSettings
            {
                Redirects = new Dictionary<string, Redirect>
                {
                    { "Student", new Redirect { GroupName = "Students", RedirectUrl = "https://example.org/Student" } },
                    { "Staff", new Redirect { GroupName = "Staff", RedirectUrl = "https://example.org/Staff" } },
                    { "AdultEd", new Redirect { GroupName = "NotSureChangeThis", RedirectUrl = "https://example.org/AdultEd" } }
                },
                DefaultRedirectUrl = "https://example.org/Default"
            };
        }

        [Fact]
        public void Redirect_Student_ReturnsCorrectRedirect()
        {
            // Arrange
            // Mock the options passed to the controller
            Mock<IOptions<RedirectSettings>> redirectSettingsMock = new Mock<IOptions<RedirectSettings>>();
            redirectSettingsMock.Setup(m => m.Value).Returns(redirectSettings);

            // Create the controller
            var controller = new PortalRedirectController(redirectSettingsMock.Object);

            // Mock the HttpContext and User 
            Mock<ClaimsPrincipal> userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(m => m.IsInRole(redirectSettings.Redirects["Student"].GroupName)).Returns(true);
            userMock.SetReturnsDefault(false);

            Mock<HttpContext> httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(m => m.User).Returns(userMock.Object);

            controller.ControllerContext.HttpContext = httpContextMock.Object;

            // Act
            IActionResult actionResult = controller.Get();

            // Assert
            RedirectResult? result = actionResult as RedirectResult;
            Assert.NotNull(result);

            Assert.Equal("https://example.org/Student", result.Url);
        }

        [Fact]
        public void Redirect_Staff_ReturnsCorrectRedirect()
        {
            // Arrange
            // Mock the options passed to the controller
            Mock<IOptions<RedirectSettings>> redirectSettingsMock = new Mock<IOptions<RedirectSettings>>();
            redirectSettingsMock.Setup(m => m.Value).Returns(redirectSettings);

            // Create the controller
            var controller = new PortalRedirectController(redirectSettingsMock.Object);

            // Mock the HttpContext and User 
            Mock<ClaimsPrincipal> userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(m => m.IsInRole(redirectSettings.Redirects["Staff"].GroupName)).Returns(true);
            userMock.SetReturnsDefault(false);

            Mock<HttpContext> httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(m => m.User).Returns(userMock.Object);

            controller.ControllerContext.HttpContext = httpContextMock.Object;

            // Act
            IActionResult actionResult = controller.Get();

            // Assert
            RedirectResult? result = actionResult as RedirectResult;
            Assert.NotNull(result);

            Assert.Equal("https://example.org/Staff", result.Url);
        }

        [Fact]
        public void Redirect_AdultEd_ReturnsCorrectRedirect()
        {
            // Arrange
            // Mock the options passed to the controller
            Mock<IOptions<RedirectSettings>> redirectSettingsMock = new Mock<IOptions<RedirectSettings>>();
            redirectSettingsMock.Setup(m => m.Value).Returns(redirectSettings);

            // Create the controller
            var controller = new PortalRedirectController(redirectSettingsMock.Object);

            // Mock the HttpContext and User 
            Mock<ClaimsPrincipal> userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(m => m.IsInRole(redirectSettings.Redirects["AdultEd"].GroupName)).Returns(true);
            userMock.SetReturnsDefault(false);

            Mock<HttpContext> httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(m => m.User).Returns(userMock.Object);

            controller.ControllerContext.HttpContext = httpContextMock.Object;

            // Act
            IActionResult actionResult = controller.Get();

            // Assert
            RedirectResult? result = actionResult as RedirectResult;
            Assert.NotNull(result);

            Assert.Equal("https://example.org/AdultEd", result.Url);
        }

        [Fact]
        public void Redirect_NoGroups_ReturnsCorrectRedirect()
        {
            // Arrange
            // Mock the options passed to the controller
            Mock<IOptions<RedirectSettings>> redirectSettingsMock = new Mock<IOptions<RedirectSettings>>();
            redirectSettingsMock.Setup(m => m.Value).Returns(redirectSettings);

            // Create the controller
            var controller = new PortalRedirectController(redirectSettingsMock.Object);

            // Mock the HttpContext and User 
            Mock<ClaimsPrincipal> userMock = new Mock<ClaimsPrincipal>();
            userMock.SetReturnsDefault(false);

            Mock<HttpContext> httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(m => m.User).Returns(userMock.Object);

            controller.ControllerContext.HttpContext = httpContextMock.Object;

            // Act
            IActionResult actionResult = controller.Get();

            // Assert
            RedirectResult? result = actionResult as RedirectResult;
            Assert.NotNull(result);

            Assert.Equal("https://example.org/Default", result.Url);
        }
    }
}