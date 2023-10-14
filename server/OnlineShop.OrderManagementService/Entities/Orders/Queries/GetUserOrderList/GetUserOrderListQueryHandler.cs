using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.OrderManagementService.Data;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetUserOrderList
{
    public class GetUserOrderListQueryHandler
        : IRequestHandler<GetUserOrderListQuery, IList<OrderLookupDto>>
    {
        readonly IOrderDbContext _context;
        readonly IMapper _mapper;

        public GetUserOrderListQueryHandler
            (IOrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<OrderLookupDto>> Handle(GetUserOrderListQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(x => x.Items)
                .Where(x => x.UserId == request.UserId)
                .ProjectTo<OrderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
