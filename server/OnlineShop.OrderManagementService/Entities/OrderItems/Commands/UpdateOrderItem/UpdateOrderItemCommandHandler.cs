using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.OrderManagementService.Data;
using OnlineShop.OrderManagementService.Entities.OrderItems.Commands.CreateOrderItem;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Entities.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommandHandler
        : IRequestHandler<UpdateOrderItemCommand>
    {
        readonly IOrderDbContext _context;

        public UpdateOrderItemCommandHandler
            (IOrderDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateOrderItemCommand request,
            CancellationToken cancellationToken)
        {
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (orderItem == null)
            {
                throw new EntityNotFoundException(nameof(OrderItem), request.Id);
            }

            orderItem.ProductId = request.ProductId;
            orderItem.ProductName = request.ProductName;
            orderItem.ProductPrice = request.ProductPrice;
            orderItem.ProductQuantity = request.ProductQuantity;

            await _context.SaveChangesAsync();;
        }
    }
}
