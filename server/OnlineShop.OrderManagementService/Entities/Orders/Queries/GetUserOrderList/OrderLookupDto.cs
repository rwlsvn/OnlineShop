using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.OrderManagementService.Models;
using System.Security.Cryptography.X509Certificates;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderList
{
    public class OrderLookupDto : IMapWith<Order>
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItemLookupDto> Items { get; set; }

        public void Mapping(Profile profile)
        { 
            profile.CreateMap<Order, OrderLookupDto>();
        }
    }
}
