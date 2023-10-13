using OnlineShop.ProductManagementService.Models;

namespace OnlineShop.ProductManagementService.Tests.Data
{
    internal static class SeedData
    {
        public static readonly Category CategoryA = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category-A"
        };

        public static readonly Category CategoryB = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category-B"
        };

        public static readonly Product ProductA = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = CategoryA.Id,
            Name = "Product-A",
            Description = "Description-A",
            Price = 100,
            ImageName = "25b8b5b4-eb93-49a0-ba60-d53eb1b87737.png"
        };

        public static readonly Product ProductB = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = CategoryB.Id,
            Name = "Product-B",
            Description = "Description-B",
            Price = 200,
            ImageName = "2cf15ff5-5a51-41a7-aa34-e99e6f9e8aaa.png"
        };
    }
}
