using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderList;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderDetails
{
    public class UserOrderDetailsVm : IMapWith<Order>
    {
        public Guid Id { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string? RecipientEmail { get; set; }
        public string RecipientPhone { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Status { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddresss { get; set; }
        public ICollection<OrderItemLookupDto> Items { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, UserOrderDetailsVm>();
        }
    }
}
