using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.OrderManagementService.Data;
using OnlineShop.OrderManagementService.Entities.Orders.Commands.CreateOrder;
using OnlineShop.OrderManagementService.Models;
using System.Diagnostics.Metrics;

namespace OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler
        : IRequestHandler<UpdateOrderCommand>
    {
        readonly IOrderDbContext _context;

        public UpdateOrderCommandHandler
            (IOrderDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (order == null) 
            { 
                throw new EntityNotFoundException(nameof(Order), request.Id);
            }

            order.RecipientFirstName = request.RecipientFirstName;
            order.RecipientLastName = request.RecipientLastName;
            order.RecipientEmail = request.RecipientEmail;
            order.RecipientPhone = request.RecipientPhone;
            order.Country = request.Country;
            order.City = request.City;
            order.PostalCode = request.PostalCode;
            order.StreetAddress = request.StreetAddress;
            await _context.SaveChangesAsync();
        }
    }
}
