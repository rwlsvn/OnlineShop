using MediatR;

namespace OnlineShop.OrderManagementService.Entities.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}