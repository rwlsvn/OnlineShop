using System.Security.Claims;

namespace OnlineShop.IdentityService.Services
{
    public interface ITokenBuilderService
    {
        public string CreateToken(string id, string email, IEnumerable<string> roles);
        public string CreateRefreshToken();
        public ClaimsPrincipal GetPrincipleFromExpiredToken(string token);
    }
}
