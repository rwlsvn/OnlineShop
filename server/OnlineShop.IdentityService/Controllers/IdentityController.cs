using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.IdentityService.Models;
using OnlineShop.IdentityService.Repositories;
using OnlineShop.IdentityService.Services;
using OnlineShop.Library.Models;
using System.Security.Claims;

namespace OnlineShop.IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IUserIdentityRepository _userIdentityRepository;
        private ITokenBuilderService _tokenBuilder;

        public IdentityController(ITokenBuilderService tokenBuilder, IUserIdentityRepository userIdentityRepository,
            UserManager<AppUser> userManager)
        {
            _userIdentityRepository = userIdentityRepository;
            _tokenBuilder = tokenBuilder;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin loginUserModel)
        {
            var user = await _userIdentityRepository.ValidateUserByEmail(loginUserModel);
            if (user != null)
            {
                var token = _tokenBuilder
                    .CreateToken(user.Id, user.Email, await _userManager.GetRolesAsync(user));

                var refreshToken = _tokenBuilder.CreateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
                await _userManager.UpdateAsync(user);

                return Ok(new TokenModel
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                });
            }
            return BadRequest("Invalid Login or Password");
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenModel tokenDto)
        {
            if (tokenDto == null)
            {
                return BadRequest("Invalid Client Request");
            }
            var principal = _tokenBuilder.GetPrincipleFromExpiredToken(tokenDto.AccessToken);
            var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.Users.FirstOrDefaultAsync(_ => _.Id == userId);

            if (user == null || user.RefreshToken != tokenDto.RefreshToken
                || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid Client Request");
            }
            var newJwtToken = _tokenBuilder
                    .CreateToken(user.Id, user.Email, await _userManager.GetRolesAsync(user));
            var newRefreshToken = _tokenBuilder.CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new TokenModel
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
