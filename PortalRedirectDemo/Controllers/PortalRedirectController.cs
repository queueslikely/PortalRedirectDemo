using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PortalRedirectDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PortalRedirectController : ControllerBase
    {
        private RedirectSettings redirectSettings;

        public PortalRedirectController(IOptions<RedirectSettings> redirectSettings)
        {
            this.redirectSettings = redirectSettings.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Student
            if (User.IsInRole(redirectSettings.Redirects["Student"].GroupName))
            {
                return Redirect(redirectSettings.Redirects["Student"].RedirectUrl);
            }

            // Staff
            if (User.IsInRole(redirectSettings.Redirects["Staff"].GroupName))
            {
                return Redirect(redirectSettings.Redirects["Staff"].RedirectUrl);
            }

            // Adult Ed
            if (User.IsInRole(redirectSettings.Redirects["AdultEd"].GroupName))
            {
                return Redirect(redirectSettings.Redirects["AdultEd"].RedirectUrl);
            }

            // The user isn't in any of the above groups, so redirect them elsewhere
            return Redirect(redirectSettings.DefaultRedirectUrl);
        }
    }
}
