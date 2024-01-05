# PortalRedirectDemo
[![.NET](https://github.com/queueslikely/PortalRedirectDemo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/queueslikely/PortalRedirectDemo/actions/workflows/dotnet.yml)

Quick and dirty bodge to redirect a user to the correct SharePoint site depending on their AD group membership. Got bored, added tests and removed some hardcoded stuff (using the options pattern).

Things to consider:
- I'm not sure if using the SID instead of the group name is more performant. Worth taking a look at?
- Is Seamless SSO possible? Right now you have to type in your email address at the SharePoint login prompt which isn't exactly a great user experience for a home page.
- Broken on Firefox but I think this is to do with Kerberos & the WWW-Authenticate header not being NTLM (probably a good thing)
