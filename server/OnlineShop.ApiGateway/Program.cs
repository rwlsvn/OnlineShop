using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();
await Configure(app);

app.Run();

void RegisterServices(IServiceCollection services)
{
    builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
    services.AddOcelot(builder.Configuration);
}

async Task Configure(IApplicationBuilder app)
{
    await app.UseOcelot();
}
