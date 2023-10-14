using MediatR;

namespace OnlineShop.OrderManagementService.Entities.OrderItems.Commands.DeleteOrderItem
{
    public class DeleteOrderItemCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
