using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderDetails
{
    public class OrderItemLookupDto : IMapWith<OrderItem>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderItem, OrderItemLookupDto>();
        }
    }
}
