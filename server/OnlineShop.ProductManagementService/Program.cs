using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductManagementService.Data;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();
Configire(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddControllers();

    services.AddDbContext<IProductsDbContext, ProductsDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration
            .GetConnectionString("ProductsDbConnection"));
    });
}

void Configire(IApplicationBuilder app)
{
    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
