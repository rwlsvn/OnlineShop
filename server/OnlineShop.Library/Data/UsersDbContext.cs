using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Models;

namespace OnlineShop.Library.Data
{
    public class UsersDbContext : IdentityDbContext<AppUser>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
