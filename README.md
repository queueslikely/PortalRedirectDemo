# PortalRedirectDemo
[![.NET](https://github.com/queueslikely/PortalRedirectDemo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/queueslikely/PortalRedirectDemo/actions/workflows/dotnet.yml)

Quick and dirty bodge to redirect a user to the correct SharePoint site depending on their AD group membership. Got bored at the weekend, added tests and removed some hardcoded stuff.

Things to consider:
- I'm not sure if using the SID instead of the group name is more performant. Worth taking a look at?
- Is Seamless SSO possible? Right now you have to type in your email address at the SharePoint login prompt which isn't exactly a great user experience for a home page.
- Broken on Firefox (![fix](https://docs.netscaler.com/en-us/citrix-adc/current-release/aaa-tm/configuring-commonly-used-protocols/citrix-adc-aaa-with-kerberos-ntlm/kerberos-config-on-client.html))
