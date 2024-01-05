namespace PortalRedirectDemo.Models
{
    public class RedirectSettings
    {
        public required Dictionary<string, Redirect> Redirects { get; set; }
        public required string DefaultRedirectUrl { get; set; }
    }

    public class Redirect
    {
        public required string GroupName { get; set; }
        public required string RedirectUrl { get; set; }
    }
}
