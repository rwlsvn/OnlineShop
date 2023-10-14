using MediatR;

namespace OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public Guid Id { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string? RecipientEmail { get; set; }
        public string RecipientPhone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddresss { get; set; }
    }
}
