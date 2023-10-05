using Microsoft.AspNetCore.Identity;
using OnlineShop.IdentityService.Models;
using OnlineShop.Library.Models;

namespace OnlineShop.IdentityService.Repositories
{
    public class UserIdentityRepository : IUserIdentityRepository
    {
        private UserManager<AppUser> _userManager;

        public UserIdentityRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> ValidateUserByEmail(UserLogin userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            var result = user != null && await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
            return result ? user : null;
        }
    }
}
