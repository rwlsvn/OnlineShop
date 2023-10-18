using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrderStatus;

namespace OnlineShop.OrderManagementService.Models.Dto
{
    public class UpdateOrderStatusDto : IMapWith<UpdateOrderStatusCommand>
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderStatusDto, UpdateOrderStatusCommand>();
        }
    }
}
