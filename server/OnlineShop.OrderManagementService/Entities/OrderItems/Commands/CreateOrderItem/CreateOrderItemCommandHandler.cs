using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.OrderManagementService.Data;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Entities.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommandHandler
        : IRequestHandler<CreateOrderItemCommand, Guid>
    {
        readonly IOrderDbContext _context;

        public CreateOrderItemCommandHandler
            (IOrderDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateOrderItemCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == request.OrderId);

            if (order == null) 
            { 
                throw new EntityNotFoundException(nameof(Order), request.OrderId);
            }

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                ProductPrice = request.ProductPrice,
                ProductQuantity = request.ProductQuantity
            };
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return orderItem.Id;
        }
    }
}