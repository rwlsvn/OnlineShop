using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Tests.Data
{
    public static class SeedData
    {
        public static readonly Guid UserAId = Guid.NewGuid();

        public static readonly Order OrderA = new Order
        {
            Id = Guid.NewGuid(),
            UserId = UserAId,
            RecipientFirstName = "AName",
            RecipientLastName = "ALastName",
            RecipientEmail = "usera@gmail.com",
            RecipientPhone = "2063428631",
            Country = "CountryA",
            City = "CityA",
            PostalCode = "1111",
            StreetAddress = "StreetA",
            CreationDate = DateTime.Now,
            Status = OrderStatus.New,
        };

        public static readonly OrderItem OrderItemA = new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = OrderA.Id,
            ProductId = Guid.NewGuid(),
            ProductName = "ProductNameA",
            ProductPrice = 99.99M,
            ProductQuantity = 2
        };

        public static readonly OrderItem OrderItemB = new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = OrderA.Id,
            ProductId = Guid.NewGuid(),
            ProductName = "ProductNameB",
            ProductPrice = 55.55M,
            ProductQuantity = 3
        };
    }
}
