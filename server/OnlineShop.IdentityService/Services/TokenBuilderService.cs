using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.IdentityService.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShop.IdentityService.Services
{
    public class TokenBuilderService : ITokenBuilderService
    {
        private TimeSpan _expirityDuration = TimeSpan.FromSeconds(20);

        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;

        public TokenBuilderService(IOptions<JwtConfiguration> options)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
            _issuer = options.Value.Issuer;
        }

        public string CreateToken(string id, string email, IEnumerable<string> roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, id));
            claims.Add(new Claim(ClaimTypes.Email, email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var jwt = new JwtSecurityToken(
                claims: claims,
                issuer: _issuer,
                expires: DateTime.UtcNow.Add(_expirityDuration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,

                ValidateAudience = false,
                ValidateLifetime = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null)
                throw new SecurityTokenException("Invalid Token");
            return principal;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
