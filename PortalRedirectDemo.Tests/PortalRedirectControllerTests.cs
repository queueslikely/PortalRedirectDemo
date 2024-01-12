using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using PortalRedirectDemo.Controllers;
using PortalRedirectDemo.Models;
using System.Security.Claims;
using Xunit;

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
                    { "Student", new Redirect { GroupSID = "S-1-5-21-1547161642-413027322-682003330-65676", RedirectUrl = "https://example.com/Student" } },
                    { "Staff", new Redirect { GroupSID = "S-1-5-21-1547161642-413027322-682003330-1439", RedirectUrl = "https://example.com/Staff" } },
                    { "AdultEd", new Redirect { GroupSID = "S-1-5-21-1547161642-413027322-682003330-80161", RedirectUrl = "https://example.com/AdultEd" } }
                },
                DefaultRedirectUrl = "https://example.com/Default"
            };
        }

        [Theory]
        [InlineData("S-1-5-21-1547161642-413027322-682003330-65676")] // Student
        [InlineData("S-1-5-21-1547161642-413027322-682003330-1439")] // Staff
        [InlineData("S-1-5-21-1547161642-413027322-682003330-80161")] // Adult Ed
        public void Redirect_Groups_ReturnsCorrectRedirect(string testGroup)
        {
            // Arrange
            // Mock the options passed to the controller
            Mock<IOptions<RedirectSettings>> redirectSettingsMock = new Mock<IOptions<RedirectSettings>>();
            redirectSettingsMock.Setup(m => m.Value).Returns(redirectSettings);

            // Create the controller
            var controller = new PortalRedirectController(redirectSettingsMock.Object);

            // Mock the HttpContext and User 
            Mock<ClaimsPrincipal> userMock = new Mock<ClaimsPrincipal>();
            userMock.Setup(m => m.IsInRole(redirectSettings.Redirects[testGroup].GroupSID)).Returns(true);

            Mock<HttpContext> httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(m => m.User).Returns(userMock.Object);

            controller.ControllerContext.HttpContext = httpContextMock.Object;

            // Act
            IActionResult actionResult = controller.Get();

            // Assert
            RedirectResult? result = actionResult as RedirectResult;
            Assert.NotNull(result);

            Assert.Equal($"https://example.com/{testGroup}", result.Url);
        }

        [Fact]
        public void Redirect_NoGroups_ReturnsDefaultRedirect()
        {
            // Arrange
            // Mock the options passed to the controller
            Mock<IOptions<RedirectSettings>> redirectSettingsMock = new Mock<IOptions<RedirectSettings>>();
            redirectSettingsMock.Setup(m => m.Value).Returns(redirectSettings);

            // Create the controller
            var controller = new PortalRedirectController(redirectSettingsMock.Object);

            // Mock the HttpContext and User 
            Mock<ClaimsPrincipal> userMock = new Mock<ClaimsPrincipal>();

            Mock<HttpContext> httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(m => m.User).Returns(userMock.Object);

            controller.ControllerContext.HttpContext = httpContextMock.Object;

            // Act
            IActionResult actionResult = controller.Get();

            // Assert
            RedirectResult? result = actionResult as RedirectResult;
            Assert.NotNull(result);

            Assert.Equal($"https://example.com/Default", result.Url);
        }
    }
}
