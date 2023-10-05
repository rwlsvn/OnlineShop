using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Library.Models;
using OnlineShop.UserManagementService.Controllers.Base;
using OnlineShop.UserManagementService.Models;

namespace OnlineShop.UserManagementService.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : BaseController
    {
        private UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IdentityResult> Register(RegisterUserDto registerUserModel)
        {
            var appUser = new AppUser()
            {
                UserName = registerUserModel.UserName,
                Email = registerUserModel.Email,
                FirstName = registerUserModel.FirstName,
                LastName = registerUserModel.LastName
            };
            var result = await _userManager.CreateAsync(appUser, registerUserModel.Password);
            return result;
        }

        [HttpGet("get")]
        public async Task<UserDto> Get()
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());

            var userInfo = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return userInfo;
        }

        [HttpPut("update")]
        public async Task<IdentityResult> Update(UpdateUserDto updateUser)
        {
            var userToBeUpdated = await _userManager.FindByIdAsync(UserId.ToString());

            userToBeUpdated.Email = userToBeUpdated.Email;
            userToBeUpdated.UserName = userToBeUpdated.UserName;
            userToBeUpdated.FirstName = updateUser.FirstName;
            userToBeUpdated.LastName = updateUser.LastName;

            var result = await _userManager.UpdateAsync(userToBeUpdated);
            return result;
        }

        [HttpPut("changepassword")]
        public async Task<IdentityResult> ChangePassword(ChangePasswordDto changePasswordModel)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            var result = await _userManager.ChangePasswordAsync
                (user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);

            return result;
        }

        [HttpDelete("remove")]
        [Authorize(Roles = "admin")]
        public async Task<IdentityResult> RemoveByUserName(string userName)
        {
            var userToBeDeleted = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.DeleteAsync(userToBeDeleted);

            return result;
        }

        [HttpPost("addrole")]
        [Authorize(Roles = "admin")]
        public async Task<IdentityResult> AddRole(AddRoleToUserDto addRoleToUser)
        {
            var user = await _userManager.FindByNameAsync(addRoleToUser.UserName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError() { Description = $"User {addRoleToUser.UserName} was not found." });
            }
            var result = await _userManager.AddToRoleAsync(user, addRoleToUser.RoleName);

            return result;
        }

        [HttpPost("removerole")]
        [Authorize(Roles = "admin")]
        public async Task<IdentityResult> RemoveRole(AddRoleToUserDto addRoleToUser)
        {
            var user = await _userManager.FindByNameAsync(addRoleToUser.UserName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError() { Description = $"User {addRoleToUser.UserName} was not found." });
            }
            var result = await _userManager.RemoveFromRoleAsync(user, addRoleToUser.RoleName);

            return result;
        }

        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        public async Task<IEnumerable<AppUser>> GetAll()
        {
            var result = _userManager.Users.AsEnumerable();
            return result;
        }
    }
}
