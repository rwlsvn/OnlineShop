using MediatR;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
