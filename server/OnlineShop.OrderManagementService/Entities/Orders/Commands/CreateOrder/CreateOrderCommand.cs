using MediatR;

namespace OnlineShop.OrderManagementService.Entities.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string? RecipientEmail { get; set; }
        public string RecipientPhone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }
    }
}
