using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OnlineShop.ProductManagementService.Models.Dto;
using OnlineShop.ProductManagementService.Tests.Data;
using OnlineShop.ProductManagementService.Tests.Helpers;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace OnlineShop.ProductManagementService.Tests.IntegrationTests
{
    public class CategoryIntegrationTest : IDisposable
    {
        ProductWebApplicationFactory _factory;
        HttpClient _client;
        public CategoryIntegrationTest()
        {
            _factory = new ProductWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        public void Dispose()
        {
            _factory.Dispose();
        }

        [Fact]
        public async Task WHEN_GetAllCategoriesRequest_THEN_AllCategoriesAreRetrieved()
        {
            var response = await _client.GetAsync("/api/category/all");
            var jsonArray = JArray.Parse(await response.Content.ReadAsStringAsync());
            int expectedArrayLength = _factory.Context.Categories.Count();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
            Assert.Equal(expectedArrayLength, jsonArray.Count);
        }


        [Fact]
        public async Task WHEN_AddNewCategoryRequest_THEN_CategoryIsAddedToDatabase()
        {
            var createCategoryDto = new CreateCategoryDto
            {
                Name = Guid.NewGuid().ToString(),
            };

            string jsonPayload = JsonSerializer.Serialize(createCategoryDto);

            var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/category/add", content);
            var categoryId = await response.Content.ReadFromJsonAsync<Guid>();

            var category = await _factory.Context.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(Guid.Empty, categoryId);
            Assert.NotNull(category);
            Assert.Equal(createCategoryDto.Name, category.Name);
        }

        private static IEnumerable<object[]> UpdateCategoryTestData()
        {
            yield return new object[] { SeedData.CategoryA.Id, HttpStatusCode.NoContent };
            yield return new object[] { Guid.NewGuid(), HttpStatusCode.NotFound };
        }

        [Theory]
        [MemberData("UpdateCategoryTestData")]
        public async Task WHEN_UpdateCategoryRequest_THEN_CategoryIsUpdatedInDatabase
           (Guid categoryId, HttpStatusCode expectedStatusCode)
        {
            var updateCategoryDto = new UpdateCategoryDto
            {
                Id = categoryId,
                Name = Guid.NewGuid().ToString(),
            };

            string jsonPayload = JsonSerializer.Serialize(updateCategoryDto);

            var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/category/update", content);
          
            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                var category = await _factory.Context.Categories
                    .FirstOrDefaultAsync(x => x.Id == categoryId);
                _factory.Context.Entry(category).Reload();

                Assert.NotNull(category);
                Assert.Equal(category.Name, updateCategoryDto.Name);
            }
        }

        private static IEnumerable<object[]> DeleteCategoryTestData()
        {
            yield return new object[] { SeedData.CategoryA.Id, HttpStatusCode.NoContent };
            yield return new object[] { Guid.NewGuid(), HttpStatusCode.NotFound };
        }

        [Theory]
        [MemberData("DeleteCategoryTestData")]
        public async Task WHEN_DeleteCategoryRequest_THEN_CategoryIsDeletedFromDatabase
           (Guid categoryId, HttpStatusCode expectedStatusCode)
        {
            var response = await _client.DeleteAsync($"/api/category/delete/{categoryId}");

            var category = await _factory.Context.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId);

            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                Assert.Null(category);
            }
        }
    }
}
