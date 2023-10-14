using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.OrderManagementService.Data;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Entities.OrderItems.Commands.DeleteOrderItem
{
    public class DeleteOrderItemCommandHandler
        : IRequestHandler<DeleteOrderItemCommand>
    {
        readonly IOrderDbContext _context;

        public DeleteOrderItemCommandHandler
            (IOrderDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteOrderItemCommand request,
            CancellationToken cancellationToken)
        {
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (orderItem == null)
            {
                throw new EntityNotFoundException(nameof(OrderItem), request.Id);
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
