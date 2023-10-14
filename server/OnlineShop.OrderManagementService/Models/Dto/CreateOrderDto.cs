using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.OrderManagementService.Entities.Orders.Commands.CreateOrder;

namespace OnlineShop.OrderManagementService.Models.Dto
{
    public class CreateOrderDto : IMapWith<CreateOrderCommand>
    {
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string? RecipientEmail { get; set; }
        public string RecipientPhone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddresss { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderDto, CreateOrderCommand>();
        }
    }
}
