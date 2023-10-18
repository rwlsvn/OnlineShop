using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OnlineShop.OrderManagementService.Data;
using OnlineShop.OrderManagementService.Models;
using OnlineShop.OrderManagementService.Tests.Data;
using System.Security.Claims;

namespace OnlineShop.OrderManagementService.Tests.Helpers
{
    public class OrderWebApplicationFactory : WebApplicationFactory<Program>
    {
        IEnumerable<Claim> _authClaims;

        private string _storageName = Guid.NewGuid().ToString();

        public OrderDbContext Context { get; private set; }

        public OrderWebApplicationFactory(IEnumerable<Claim> authClaims)
        {
            _authClaims = authClaims;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<OrderDbContext>));

                services.AddDbContext<OrderDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_storageName);
                });

                Context = services.BuildServiceProvider()
                    .GetRequiredService<OrderDbContext>();
                SeedDbContext();

                services.AddAuthorization(options =>
                {
                    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                        "Test");
                    defaultAuthorizationPolicyBuilder =
                        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
                });

                services.AddAuthentication("Test")
                    .AddScheme<TestAuthenticationSchemeOptions, TestAuthHandler>
                        ("Test", options => { options.Claims = _authClaims; });
            });   
        }

        private void SeedDbContext()
        {
            Context.Orders.Add(
                new Order
                {
                    Id = SeedData.OrderA.Id,
                    UserId = SeedData.OrderA.UserId,
                    RecipientFirstName = SeedData.OrderA.RecipientFirstName,
                    RecipientLastName = SeedData.OrderA.RecipientLastName,
                    RecipientEmail = SeedData.OrderA.RecipientEmail,
                    RecipientPhone = SeedData.OrderA.RecipientPhone,
                    Country = SeedData.OrderA.Country,
                    City = SeedData.OrderA.City,
                    PostalCode = SeedData.OrderA.PostalCode,
                    StreetAddress = SeedData.OrderA.StreetAddress,
                    CreationDate = SeedData.OrderA.CreationDate,
                    Status = SeedData.OrderA.Status,
                }
            );

            Context.OrderItems.AddRange(
                new OrderItem
                {
                    Id = SeedData.OrderItemA.Id,
                    OrderId = SeedData.OrderItemA.OrderId,
                    ProductId = SeedData.OrderItemA.ProductId,
                    ProductName = SeedData.OrderItemA.ProductName,
                    ProductPrice = SeedData.OrderItemA.ProductPrice,
                    ProductQuantity = SeedData.OrderItemA.ProductQuantity
                },
                new OrderItem
                {
                    Id = SeedData.OrderItemB.Id,
                    OrderId = SeedData.OrderItemB.OrderId,
                    ProductId = SeedData.OrderItemB.ProductId,
                    ProductName = SeedData.OrderItemB.ProductName,
                    ProductPrice = SeedData.OrderItemB.ProductPrice,
                    ProductQuantity = SeedData.OrderItemB.ProductQuantity
                }
            );

            Context.SaveChanges();
        }
    }
}
