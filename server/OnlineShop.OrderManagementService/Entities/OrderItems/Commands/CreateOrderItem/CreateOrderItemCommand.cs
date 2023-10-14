using MediatR;

namespace OnlineShop.OrderManagementService.Entities.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}
