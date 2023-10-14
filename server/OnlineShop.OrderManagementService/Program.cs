using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Behaviors;
using OnlineShop.Library.Mapping;
using OnlineShop.Library.DependencyInjection;
using OnlineShop.OrderManagementService.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();
Configure(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddControllers();

    services.AddDbContext<IOrderDbContext, OrderDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration
            .GetConnectionString("OrdersDbConnection"));
    });

    services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IOrderDbContext).Assembly));
    });

    services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    services.AddValidatorsFromAssemblies
        (new[] { Assembly.GetExecutingAssembly() });

    services.AddTransient(typeof(IPipelineBehavior<,>),
        typeof(ValidationBehavior<,>));

    services.AddJwtAuth(builder.Configuration);
}

void Configure(IApplicationBuilder app)
{
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<CustomExceptionHandlerMiddleware>();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
