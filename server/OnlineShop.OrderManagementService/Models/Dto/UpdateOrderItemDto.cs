using AutoMapper;
using OnlineShop.Library.Mapping;
using OnlineShop.OrderManagementService.Entities.OrderItems.Commands.UpdateOrderItem;

namespace OnlineShop.OrderManagementService.Models.Dto
{
    public class UpdateOrderItemDto : IMapWith<UpdateOrderItemCommand>
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderItemDto, UpdateOrderItemCommand>();
        }
    }
}
