using MediatR;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderDetails
{
    public class GetUserOrderDetailsQuery : IRequest<UserOrderDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
