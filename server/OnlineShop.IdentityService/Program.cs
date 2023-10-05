using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.IdentityService.Configuration;
using OnlineShop.IdentityService.Repositories;
using OnlineShop.IdentityService.Services;
using OnlineShop.Library.Data;
using OnlineShop.Library.Models;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();
Configure(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddControllers();

    services.AddDbContext<UsersDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("UsersDbConnection"));
    });

    services.AddIdentity<AppUser, IdentityRole>()
        .AddEntityFrameworkStores<UsersDbContext>()
        .AddDefaultTokenProviders();

    services.AddTransient<ITokenBuilderService, TokenBuilderService>();
    services.AddTransient<IUserIdentityRepository, UserIdentityRepository>();

    services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));
}

void Configure(IApplicationBuilder app)
{
    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
