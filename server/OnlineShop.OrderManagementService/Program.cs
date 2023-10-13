using Microsoft.EntityFrameworkCore;
using OnlineShop.OrderManagementService.Data;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();
Configure(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddDbContext<IOrderDbContext, OrderDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration
            .GetConnectionString("OrdersDbConnection"));
    });
}

void Configure(IApplicationBuilder app)
{

}
