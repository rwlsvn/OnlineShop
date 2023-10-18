using Microsoft.EntityFrameworkCore;
using OnlineShop.OrderManagementService.Models.Dto;
using OnlineShop.OrderManagementService.Tests.Data;
using OnlineShop.OrderManagementService.Tests.Helpers;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Xunit;

namespace OnlineShop.OrderManagementService.Tests.IntegrationTests
{
    public class OrderItemAdminIntegrationTests
    {
        OrderWebApplicationFactory _adminFactory;

        HttpClient _adminClient;

        public OrderItemAdminIntegrationTests()
        {
            _adminFactory = new OrderWebApplicationFactory(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "admin")
                });

            _adminClient = _adminFactory.CreateClient();
        }

        private static IEnumerable<object[]> OrderItemTestData()
        {
            yield return new object[] { SeedData.OrderItemA.Id, HttpStatusCode.NoContent };
            yield return new object[] { Guid.NewGuid(), HttpStatusCode.NotFound };
        }

        [Theory]
        [MemberData("OrderItemTestData")]
        public async Task WHEN_UpdateOrderItemRequest_THEN_OrderItemIsUpdatedInDatabase
            (Guid orderId, HttpStatusCode expectedStatusCode)
        {
            var updateOrderItemDto = new UpdateOrderItemDto
            {
                Id = orderId,
                ProductId = Guid.NewGuid(),
                ProductName = "Updated Product Name",
                ProductPrice = 99.99M,
                ProductQuantity = 3
            };

            string jsonPayload = JsonSerializer.Serialize(updateOrderItemDto);
            var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
            var response = await _adminClient.PutAsync("/api/admin/orderitem/update", content);

            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                var orderItem = await _adminFactory.Context.OrderItems
                    .FirstOrDefaultAsync(x => x.Id == orderId);

                _adminFactory.Context.Entry(orderItem).Reload();

                Assert.NotNull(orderItem);

                Assert.Equal(updateOrderItemDto.ProductId, orderItem.ProductId);
                Assert.Equal(updateOrderItemDto.ProductName, orderItem.ProductName);
                Assert.Equal(updateOrderItemDto.ProductPrice, orderItem.ProductPrice);
                Assert.Equal(updateOrderItemDto.ProductQuantity, orderItem.ProductQuantity);
            }
        }

        [Theory]
        [MemberData("OrderItemTestData")]
        public async Task WHEN_DeleteOrderItemTestRequest_THEN_OrderItemIsDeletedFromDatabase
           (Guid orderId, HttpStatusCode expectedStatusCode)
        {
            var response = await _adminClient.DeleteAsync($"/api/admin/orderitem/delete/{orderId}");

            var orderItem = await _adminFactory.Context.OrderItems
                .FirstOrDefaultAsync(x => x.Id == orderId);

            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Null(orderItem);         
        }
    }
}
