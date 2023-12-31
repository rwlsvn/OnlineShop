﻿using Microsoft.EntityFrameworkCore;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.Common;
using OnlineShop.OrderManagementService.Models;
using OnlineShop.OrderManagementService.Models.Dto;
using OnlineShop.OrderManagementService.Tests.Data;
using OnlineShop.OrderManagementService.Tests.Helpers;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Xunit;

namespace OnlineShop.OrderManagementService.Tests.IntegrationTests
{
    public class OrderAdminIntegrationTests
    {
        OrderWebApplicationFactory _adminFactory;

        HttpClient _adminClient;

        public OrderAdminIntegrationTests()
        {
            _adminFactory = new OrderWebApplicationFactory(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "admin")
                });

            _adminClient = _adminFactory.CreateClient();
        }

        private static IEnumerable<object[]> GetOrderListTestData()
        {
            yield return new object[] 
            { 
                new GetOrderListDto { UserId = SeedData.UserAId } 
            };
            yield return new object[] 
            { 
                new GetOrderListDto { RecipientFirstName = SeedData.OrderA.RecipientFirstName } 
            };
            yield return new object[] 
            { 
                new GetOrderListDto { RecipientLastName = SeedData.OrderA.RecipientLastName } 
            };
            yield return new object[] 
            { 
                new GetOrderListDto { RecipientEmail = SeedData.OrderA.RecipientEmail } 
            };
            yield return new object[] 
            { 
                new GetOrderListDto { RecipientPhone = SeedData.OrderA.RecipientPhone } 
            };
        }

        [Theory]
        [MemberData("GetOrderListTestData")]
        public async Task WHEN_GetOrdersRequest_THEN_OrdersWithFiltersAreRetrieved
            (GetOrderListDto query)
        {
            string jsonPayload = JsonSerializer.Serialize(query);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_adminClient.BaseAddress + "api/admin/order/get"),
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            var response = await _adminClient.SendAsync(request).ConfigureAwait(false);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var ordersList = Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<OrderLookupDto>>(jsonResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            foreach (var order in ordersList)
            {
                var orderDetails = await _adminFactory.Context.Orders
                    .FirstOrDefaultAsync(x => x.Id == order.Id 
                        && (query.UserId == null || x.UserId == query.UserId)
                        && (query.RecipientFirstName == null 
                            || x.RecipientFirstName == query.RecipientFirstName)
                        && (query.RecipientLastName == null 
                            || x.RecipientLastName == query.RecipientLastName)
                        && (query.RecipientEmail == null 
                            || x.RecipientEmail == query.RecipientEmail)
                        && (query.RecipientPhone == null 
                            || x.RecipientPhone == query.RecipientPhone));

                Assert.NotNull(orderDetails);
            }
        }

        private static IEnumerable<object[]> GetOrderDetailsTestData()
        {
            yield return new object[] { SeedData.OrderA.Id, HttpStatusCode.OK };
            yield return new object[] { Guid.NewGuid(), HttpStatusCode.NotFound };
        }

        [Theory]
        [MemberData("GetOrderDetailsTestData")]
        public async Task WHEN_GetOrderByIdRequest_THEN_OrderIsRetrieved
           (Guid orderId, HttpStatusCode expectedStatusCode)
        {
            var response = await _adminClient.GetAsync($"/api/admin/order/get/{orderId}");

            var order = await _adminFactory.Context.Orders
                    .FirstOrDefaultAsync(x => x.Id == orderId);

            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == HttpStatusCode.OK)
                Assert.NotNull(order);

            if (response.StatusCode == HttpStatusCode.NotFound)
                Assert.Null(order);
        }

        private static IEnumerable<object[]> UpdateOrderTestData()
        {
            yield return new object[] { SeedData.OrderA.Id, HttpStatusCode.NoContent };
            yield return new object[] { Guid.NewGuid(), HttpStatusCode.NotFound };
        }

        [Theory]
        [MemberData("UpdateOrderTestData")]
        public async Task WHEN_UpdateOrderRequest_THEN_OrderIsUpdatedInDatabase
            (Guid orderId, HttpStatusCode expectedStatusCode)
        {
            var updateOrderDto = new UpdateOrderDto
            {
                Id = orderId,
                RecipientFirstName = "NewName",
                RecipientLastName = "NewLastName",
                RecipientEmail = "newemail@gmail.com",
                RecipientPhone = "11122233355",
                Country = "NewCountry",
                City = "NewCity",
                PostalCode = "3333",
                StreetAddress = "New Street Address"
            };

            string jsonPayload = JsonSerializer.Serialize(updateOrderDto);
            var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
            var response = await _adminClient.PutAsync("/api/admin/order/update", content);

            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                var order = await _adminFactory.Context.Orders
                    .FirstOrDefaultAsync(x => x.Id == orderId);

                _adminFactory.Context.Entry(order).Reload();

                Assert.NotNull(order);

                Assert.Equal(updateOrderDto.RecipientFirstName, order.RecipientFirstName);
                Assert.Equal(updateOrderDto.RecipientLastName, order.RecipientLastName);
                Assert.Equal(updateOrderDto.RecipientEmail, order.RecipientEmail);
                Assert.Equal(updateOrderDto.RecipientPhone, order.RecipientPhone);
                Assert.Equal(updateOrderDto.Country, order.Country);
                Assert.Equal(updateOrderDto.City, order.City);
                Assert.Equal(updateOrderDto.PostalCode, order.PostalCode);
                Assert.Equal(updateOrderDto.StreetAddress, order.StreetAddress);
            }
        }

        [Theory]
        [MemberData("UpdateOrderTestData")]
        public async Task WHEN_UpdateOrderStatusRequest_THEN_OrderStatusIsUpdatedInDatabase
            (Guid orderId, HttpStatusCode expectedStatusCode)
        {
            var updateOrderDto = new UpdateOrderStatusDto
            {
                Id = orderId,
                Status = OrderStatus.Processing
            };

            string jsonPayload = JsonSerializer.Serialize(updateOrderDto);
            var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
            var response = await _adminClient.PutAsync("/api/admin/order/updatestatus", content);

            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                var order = await _adminFactory.Context.Orders
                    .FirstOrDefaultAsync(x => x.Id == orderId);

                _adminFactory.Context.Entry(order).Reload();

                Assert.NotNull(order);

                Assert.Equal(updateOrderDto.Status, order.Status);
            }
        }
    }
}
