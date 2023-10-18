using Microsoft.EntityFrameworkCore;
using OnlineShop.OrderManagementService.Models.Dto;
using OnlineShop.OrderManagementService.Tests.Data;
using OnlineShop.OrderManagementService.Tests.Helpers;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Xunit;

namespace OnlineShop.OrderManagementService.Tests.IntegrationTests
{
    public class OrderItemUserIntegrationTests
    {
        private static IEnumerable<object[]> AddOrderItemTestData()
        {
            var userAClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, SeedData.UserAId.ToString()),
            };

            var randomUserClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            };

            yield return new object[] { userAClaims, SeedData.OrderA.Id,
                HttpStatusCode.OK };
            yield return new object[] { randomUserClaims, SeedData.OrderA.Id,
                HttpStatusCode.Forbidden };
            yield return new object[] { userAClaims, Guid.NewGuid(),
                HttpStatusCode.NotFound };
        }

        [Theory]
        [MemberData("AddOrderItemTestData")]
        public async Task WHEN_AddNewOrderItemRequest_THEN_OrderItemIsAddedToDatabase
            (IEnumerable<Claim> authClaims, Guid orderId, HttpStatusCode expectedStatusCode)
        {
            var factory = new OrderWebApplicationFactory(authClaims);
            var client = factory.CreateClient();

            var createOrderItemDto = new CreateOrderItemDto
            {
                OrderId = orderId,
                ProductId = Guid.NewGuid(),
                ProductName = "Product Name",
                ProductPrice = 14.99M,
                ProductQuantity = 1
            };

            string jsonPayload = JsonSerializer.Serialize(createOrderItemDto);
            var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/user/orderitem/add", content);
            
            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var orderItemId = await response.Content.ReadFromJsonAsync<Guid>();
                var orderItem = await factory.Context.OrderItems
                    .FirstOrDefaultAsync(x => x.Id == orderItemId);
                Assert.NotNull(orderItem);
            }
        }       
    }
}
