using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.OrderManagementService.Data;
using OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrder;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler
        : IRequestHandler<UpdateOrderStatusCommand>
    {
        readonly IOrderDbContext _context;

        public UpdateOrderStatusCommandHandler
            (IOrderDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateOrderStatusCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (order == null)
            {
                throw new EntityNotFoundException(nameof(Order), request.Id);
            }

            order.Status = request.Status;

            await _context.SaveChangesAsync();
        }
    }
}
