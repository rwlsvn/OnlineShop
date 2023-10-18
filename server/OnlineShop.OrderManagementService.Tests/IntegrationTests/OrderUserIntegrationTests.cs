using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
    public class OrderUserIntegrationTests
    {
        OrderWebApplicationFactory _userAFactory;

        HttpClient _userAClient;

        public OrderUserIntegrationTests()
        {
            _userAFactory = new OrderWebApplicationFactory(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, SeedData.UserAId.ToString()),
                });

            _userAClient = _userAFactory.CreateClient(); 
        }

        [Fact]
        public async Task WHEN_GetAllOrdersRequest_THEN_AllOrdersAreRetrieved()
        {
            var response = await _userAClient.GetAsync("/api/user/order/all");
            var jsonArray = JArray.Parse(await response.Content.ReadAsStringAsync());
            int expectedArrayLength = _userAFactory.Context.Orders
                .Select(x => x.UserId == SeedData.UserAId)
                .Count();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
            Assert.Equal(expectedArrayLength, jsonArray.Count);
        }

        private static IEnumerable<object[]> GetOrderTestData()
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
        [MemberData("GetOrderTestData")]
        public async Task WHEN_GetOrderByIdRequest_THEN_OrderIsRetrieved
            (IEnumerable<Claim> authClaims, 
                Guid orderId, HttpStatusCode expectedStatusCode)
        {
            var factory = new OrderWebApplicationFactory(authClaims);
            var client = factory.CreateClient();

            var response = await client.GetAsync($"/api/user/order/get/{orderId}");

            var order = await factory.Context.Orders
                    .FirstOrDefaultAsync(x => x.Id == orderId);

            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == HttpStatusCode.OK)
                Assert.NotNull(order);

            if (response.StatusCode == HttpStatusCode.NotFound)
                Assert.Null(order);
        }

        [Fact]
        public async Task WHEN_AddNewOrderRequest_THEN_OrderIsAddedToDatabase()
        {
            var createOrderDto = new CreateOrderDto
            {
                RecipientFirstName = "UserAName",
                RecipientLastName = "UserALastName",
                RecipientEmail = "usera@gmail.com",
                RecipientPhone = "11122233344",
                Country = "CountryA",
                City = "CityA",
                PostalCode = "1111",
                StreetAddress = "StreetA"
            };

            string jsonPayload = JsonSerializer.Serialize(createOrderDto);
            var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

            var response = await _userAClient.PostAsync("/api/user/order/add", content);
            var orderId = await response.Content.ReadFromJsonAsync<Guid>();

            var order = await _userAFactory.Context.Orders
                .FirstOrDefaultAsync(x => x.Id == orderId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(Guid.Empty, orderId);
            Assert.NotNull(order);
        }
    }
}
