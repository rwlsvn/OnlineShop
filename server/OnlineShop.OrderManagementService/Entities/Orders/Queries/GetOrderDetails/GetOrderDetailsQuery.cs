using MediatR;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.Common;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQuery : IRequest<OrderDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
