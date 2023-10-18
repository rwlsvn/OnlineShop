using MediatR;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.Common;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery : IRequest<IList<OrderLookupDto>>
    {
        public Guid? UserId { get; set; }
        public string? RecipientFirstName { get; set; }
        public string? RecipientLastName { get; set; }
        public string? RecipientEmail { get; set; }
        public string? RecipientPhone { get; set; }
    }
}
