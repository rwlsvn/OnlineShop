using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OnlineShop.ProductManagementService.Configuration;
using OnlineShop.ProductManagementService.Data;
using OnlineShop.ProductManagementService.Models;
using OnlineShop.ProductManagementService.Tests.Data;
using Xunit;

namespace OnlineShop.ProductManagementService.Tests.Helpers
{
    public class ProductWebApplicationFactory : WebApplicationFactory<Program>
    { 
        private string _storageName = Guid.NewGuid().ToString();

        public ProductsDbContext Context { get; private set; }
        public string StorageName { get => _storageName; }
        public string TestPath { get; private set; }

        public void Dispose()
        {
            var directoryInfo = new DirectoryInfo(TestPath);
            foreach (var file in directoryInfo.GetFiles())
            {
                file.Delete();
            }

            Directory.Delete(TestPath);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ProductsDbContext>));

                services.AddDbContext<ProductsDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_storageName);
                });

                Context = services.BuildServiceProvider()
                    .GetRequiredService<ProductsDbContext>();
                SeedDbContext(Context);

                services.AddAuthorization(options =>
                {
                    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                        "Test");
                    defaultAuthorizationPolicyBuilder =
                        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
                });

                services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>
                        ("Test", options => { });

                services.Configure<StaticFileConfiguration>(fs =>
                {
                    fs.ImagePath = _storageName;
                });

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                TestPath = Path.Combine(env.WebRootPath, _storageName);
                SeedImages();
            });
        }

        private void SeedDbContext(ProductsDbContext context)
        {     
            context.Categories.AddRange(
                new Category 
                { 
                    Id = SeedData.CategoryA.Id,
                    Name = SeedData.CategoryA.Name
                },
                new Category
                {
                    Id = SeedData.CategoryB.Id,
                    Name = SeedData.CategoryB.Name
                }
            );

            context.Products.AddRange(
                new Product
                {
                    Id = SeedData.ProductA.Id,
                    CategoryId = SeedData.ProductA.CategoryId,
                    Name = SeedData.ProductA.Name,
                    Description = SeedData.ProductA.Description,
                    Price = SeedData.ProductA.Price,
                    ImageName = SeedData.ProductA.ImageName
                },
                new Product
                {
                    Id = SeedData.ProductB.Id,
                    CategoryId = SeedData.ProductB.CategoryId,
                    Name = SeedData.ProductB.Name,
                    Description = SeedData.ProductB.Description,
                    Price = SeedData.ProductB.Price,
                    ImageName = SeedData.ProductB.ImageName
                }
            );

            context.SaveChanges();
        }

        private void SeedImages()
        {
            Directory.CreateDirectory(TestPath);
            File.Copy($"images/{SeedData.ProductA.ImageName}",
                $"{TestPath}/{SeedData.ProductA.ImageName}", true);
            File.Copy($"images/{SeedData.ProductB.ImageName}",
                $"{TestPath}/{SeedData.ProductB.ImageName}", true);
        }
        public async Task<Stream> GetTestImage()
        {
            var memoryStream = new MemoryStream();
            var fileStream = File.OpenRead("images/test-image.png");
            await fileStream.CopyToAsync(memoryStream);
            fileStream.Close();
            return memoryStream;
        }
    }
}
