using MediatR;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.Common;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderList
{
    public class GetUserOrderListQuery : IRequest<IList<OrderLookupDto>>
    {
        public Guid UserId { get; set; }
    }
}
