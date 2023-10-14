using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Library.Exceptions;
using OnlineShop.OrderManagementService.Data;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderDetails
{
    public class GetUserOrderDetailsQueryHandler
        : IRequestHandler<GetUserOrderDetailsQuery, UserOrderDetailsVm>
    {
        readonly IOrderDbContext _context;
        readonly IMapper _mapper;

        public GetUserOrderDetailsQueryHandler
            (IOrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserOrderDetailsVm> Handle(GetUserOrderDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (order == null)
            {
                throw new EntityNotFoundException(nameof(Order), request.Id);
            }

            if (order.UserId != request.UserId)
            {
                throw new InvalidEntityOwnershipException
                    (nameof(Order), request.Id, request.UserId);
            }

            return _mapper.Map<UserOrderDetailsVm>(order);
        }
    }
}
