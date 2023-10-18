using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrder;

namespace OnlineShop.OrderManagementService.Models.Dto
{
    public class UpdateOrderDto : IMapWith<UpdateOrderCommand>
    {
        public Guid Id { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string? RecipientEmail { get; set; }
        public string RecipientPhone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderDto, UpdateOrderCommand>();
        }
    }
}
