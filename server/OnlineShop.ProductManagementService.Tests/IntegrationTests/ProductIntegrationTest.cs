using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductList;
using OnlineShop.ProductManagementService.Models.Dto;
using OnlineShop.ProductManagementService.Tests.Data;
using OnlineShop.ProductManagementService.Tests.Helpers;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit;

namespace OnlineShop.ProductManagementService.Tests.IntegrationTests
{
    public class ProductIntegrationTest : IDisposable
    {
        ProductWebApplicationFactory _factory;
        HttpClient _client;

        public ProductIntegrationTest()
        {
            _factory = new ProductWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        public void Dispose()
        {
            _factory.Dispose();
        }

        private static IEnumerable<object[]> GetProductTestData()
        {
            yield return new object[] { SeedData.ProductA.Id, HttpStatusCode.OK };
            yield return new object[] { Guid.NewGuid(), HttpStatusCode.NotFound };
        }

        [Theory]
        [MemberData("GetProductTestData")]
        public async Task WHEN_GetProductByIdRequest_THEN_ProductIsRetrieved
            (Guid productId, HttpStatusCode expectedStatusCode)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/product/get/{productId}");
            var product = await _factory.Context.Products
                .FirstOrDefaultAsync(x => x.Id == productId);

            Assert.Equal(response.StatusCode, expectedStatusCode);

            if (response.StatusCode == HttpStatusCode.OK)
                Assert.NotNull(product);

            if (response.StatusCode == HttpStatusCode.NotFound)
                Assert.Null(product);
        }

        private static IEnumerable<object[]> GetProductListTestData()
        {
            yield return new object[]
            {
                new GetProdutcListDto { CategotyName = SeedData.CategoryA.Name }
            };
            yield return new object[]
            {
                new GetProdutcListDto { MaxPrice = (int)(SeedData.ProductA.Price + 1) }
            };
            yield return new object[]
            {
                new GetProdutcListDto { MinPrice = (int)(SeedData.ProductA.Price - 1) }
            };
            yield return new object[]
            {
                new GetProdutcListDto { Name = SeedData.ProductA.Name }
            };
            yield return new object[]
            {
                new GetProdutcListDto { }
            };
        }

        [Theory]
        [MemberData("GetProductListTestData")]
        public async Task WHEN_GetAllProductsRequest_THEN_AllProductsAreRetrieved
            (GetProdutcListDto query)
        {
            string jsonPayload = JsonSerializer.Serialize(query);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress + "api/product/get"),
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request).ConfigureAwait(false);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var ordersList = Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<ProductLookupDto>>(jsonResponse);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            foreach (var order in ordersList)
            {
                var orderDetails = await _factory.Context.Products
                    .FirstOrDefaultAsync(x => x.Id == order.Id
                        && (query.CategotyName == null
                        || x.Category.Name == query.CategotyName)
                    && (query.MinPrice == null
                        || x.Price >= query.MinPrice)
                    && (query.MaxPrice == null
                        || x.Price <= query.MaxPrice)
                    && (query.Name == null
                        || x.Name.ToLower().Contains(query.Name.ToLower())));

                Assert.NotNull(orderDetails);
            }
        }

        [Fact]
        public async Task WHEN_AddNewProductRequest_THEN_ProductIsAddedToDatabase()
        {
            var categoryId = SeedData.CategoryA.Id;
            var name = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();
            var price = 14.3M;
            var image = await TestImageHelper.GetTestImageAsync();

            var form = new MultipartFormDataContent();

            form.Add(new StringContent(categoryId.ToString()), "CategoryId");
            form.Add(new StringContent(name), "Name");
            form.Add(new StringContent(description), "Description");
            form.Add(new StringContent(price.ToString()), "Price");
            form.Add(new StreamContent(image), "Image", "test-image.png");

            var response = await _client.PostAsync("/api/product/add", form);
            var productId = await response.Content.ReadFromJsonAsync<Guid>();

            var product = await _factory.Context.Products
                .FirstOrDefaultAsync(x => x.Id == productId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(Guid.Empty, productId);
            Assert.NotNull(product);

            Assert.Equal(categoryId, product.CategoryId);
            Assert.Equal(name, product.Name);
            Assert.Equal(description, product.Description);
            Assert.Equal(price, product.Price);

            Assert.True(File.Exists($"{_factory.TestPath}/{product.ImageName}"));
        }

        private static IEnumerable<object[]> UpdateProductTestData()
        {
            yield return new object[] { SeedData.ProductA.Id, HttpStatusCode.NoContent };
            yield return new object[] { Guid.NewGuid(), HttpStatusCode.NotFound };
        }

        [Theory]
        [MemberData("UpdateProductTestData")]
        public async Task WHEN_UpdateProductRequest_THEN_ProductIsUpdatedInDatabase
            (Guid productId, HttpStatusCode expectedStatusCode)
        {
            var categoryId = SeedData.CategoryB.Id;
            var name = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();
            var price = 15.3M;
            var image = await TestImageHelper.GetTestImageAsync();

            var form = new MultipartFormDataContent();

            form.Add(new StringContent(productId.ToString()), "Id");
            form.Add(new StringContent(categoryId.ToString()), "CategoryId");
            form.Add(new StringContent(name), "Name");
            form.Add(new StringContent(description), "Description");
            form.Add(new StringContent(price.ToString()), "Price");
            form.Add(new StreamContent(image), "Image", "test-image.png");

            var response = await _client.PutAsync("/api/product/update", form);
        
            Assert.Equal(expectedStatusCode, response.StatusCode);

            if(response.StatusCode == HttpStatusCode.NoContent)
            {
                var product = await _factory.Context.Products
                    .FirstOrDefaultAsync(x => x.Id == productId);
                _factory.Context.Entry(product).Reload();

                Assert.NotNull(product);

                Assert.Equal(categoryId, product.CategoryId);
                Assert.Equal(name, product.Name);
                Assert.Equal(description, product.Description);
                Assert.Equal(price, product.Price);

                Assert.True(File.Exists($"{_factory.TestPath}/{product.ImageName}"));
                Assert.False(File.Exists($"{_factory.TestPath}/{SeedData.ProductA.ImageName}"));
            }       
        }

        private static IEnumerable<object[]> DeleteProductTestData()
        {
            yield return new object[] { SeedData.ProductA.Id, HttpStatusCode.NoContent };
            yield return new object[] { Guid.NewGuid(), HttpStatusCode.NotFound };
        }

        [Theory]
        [MemberData("DeleteProductTestData")]
        public async Task WHEN_DeleteProductRequest_THEN_ProductIsDeletedFromDatabase
            (Guid productId, HttpStatusCode expectedStatusCode)
        {
            var response = await _client.DeleteAsync($"/api/product/delete/{productId}");
            var product = await _factory.Context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            Assert.Equal(expectedStatusCode, response.StatusCode);

            if(response.StatusCode == HttpStatusCode.NoContent)
            {
                Assert.Null(product);
                Assert.False(File.Exists($"{_factory.TestPath}/{SeedData.ProductA.ImageName}"));
            }       
        }
         
    }
}
