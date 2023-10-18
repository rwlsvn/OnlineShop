using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.OrderManagementService.Data;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.Common;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryHandler 
        : IRequestHandler<GetOrderDetailsQuery, OrderDetailsVm>
    {
        readonly IOrderDbContext _context;
        readonly IMapper _mapper;

        public GetOrderDetailsQueryHandler
            (IOrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDetailsVm> Handle(GetOrderDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (order == null)
            {
                throw new EntityNotFoundException(nameof(Order), request.Id);
            }

            return _mapper.Map<OrderDetailsVm>(order);
        }
    }
}
