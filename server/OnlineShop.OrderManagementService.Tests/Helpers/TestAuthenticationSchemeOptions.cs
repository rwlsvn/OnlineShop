using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace OnlineShop.OrderManagementService.Tests.Helpers
{
    public class TestAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public IEnumerable<Claim> Claims { get; set; }
    }
}
