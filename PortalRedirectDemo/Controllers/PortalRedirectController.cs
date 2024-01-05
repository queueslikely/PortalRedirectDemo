using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PortalRedirectDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PortalRedirectController : ControllerBase
    {
        private IConfiguration configuration;

        public PortalRedirectController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Student
            if (User.IsInRole(configuration["Redirects:Student:GroupName"]))
            {
                return Redirect(configuration["Redirects:Student:RedirectUrl"]);
            }

            // Staff
            if (User.IsInRole(configuration["Redirects:Staff:GroupName"]))
            {
                return Redirect(configuration["Redirects:Staff:RedirectUrl"]);
            }

            // Adult Ed
            if (User.IsInRole(configuration["Redirects:AdultEd:GroupName"]))
            {
                return Redirect(configuration["Redirects:AdultEd:RedirectUrl"]);
            }

            // The user isn't in any of the above groups, so redirect them elsewhere
            return Redirect(configuration["DefaultRedirectUrl"]);
        }
    }
}
