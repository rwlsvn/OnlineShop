using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using OnlineShop.ProductManagementService.Data;
using OnlineShop.Library.DependencyInjection;
using System.Reflection;
using OnlineShop.Library.Mapping;
using OnlineShop.ProductManagementService.Services;
using FluentValidation;
using MediatR;
using OnlineShop.Library.Behaviors;
using OnlineShop.ProductManagementService.Middleware;
using OnlineShop.ProductManagementService.Configuration;

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

    services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IProductsDbContext).Assembly));
    });

    services.AddMediatR(cfg => 
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    services.AddValidatorsFromAssemblies
        (new[] {Assembly.GetExecutingAssembly() });

    services.AddTransient(typeof(IPipelineBehavior<,>), 
        typeof(ValidationBehavior<,>));

    services.AddScoped<IStaticFileProvider, StaticFileProvider>();

    services.AddJwtAuth(builder.Configuration);

    services.Configure<StaticFileConfiguration>
        (builder.Configuration.GetSection("StaticFileConfiguration"));
}

void Configire(IApplicationBuilder app)
{
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<CustomExceptionHandlerMiddleware>();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.UseStaticFiles();
}

public partial class Program { }
