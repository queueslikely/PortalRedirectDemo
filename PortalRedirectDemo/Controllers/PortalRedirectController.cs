using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PortalRedirectDemo.Models;

namespace PortalRedirectDemo.Controllers
{
    [Route("PortalRedirect")]
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
            // Redirect if the user is in a configured group
            foreach (KeyValuePair<string, Redirect> redirectSection in redirectSettings.Redirects)
            {
                if (User.IsInRole(redirectSection.Value.GroupName))
                {
                    return Redirect(redirectSection.Value.RedirectUrl);
                }
            }

            // The user isn't in any of the configured groups, so redirect them elsewhere
            return Redirect(redirectSettings.DefaultRedirectUrl);
        }
    }
}
