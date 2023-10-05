using OnlineShop.IdentityService.Models;
using OnlineShop.Library.Models;

namespace OnlineShop.IdentityService.Repositories
{
    public interface IUserIdentityRepository
    {
        public Task<AppUser> ValidateUserByEmail(UserLogin userLoginDto);
    }
}
