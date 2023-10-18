using MediatR;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.Common;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderDetails
{
    public class GetUserOrderDetailsQuery : IRequest<OrderDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
