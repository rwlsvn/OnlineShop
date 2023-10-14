using MediatR;
using OnlineShop.OrderManagementService.Data;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Entities.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderCommand, Guid>
    {
        readonly IOrderDbContext _context;

        public CreateOrderCommandHandler
            (IOrderDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                RecipientFirstName = request.RecipientFirstName,
                RecipientLastName = request.RecipientLastName,
                RecipientEmail = request.RecipientEmail,
                RecipientPhone = request.RecipientPhone,
                Country = request.Country,
                City = request.City,
                PostalCode = request.PostalCode,
                StreetAddresss = request.StreetAddresss,
                CreationDate = DateTime.Now,
                Status = OrderStatus.New
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }
    }
}
