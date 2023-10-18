using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.OrderManagementService.Data;
using OnlineShop.OrderManagementService.Entities.Orders.Queries.Common;

namespace OnlineShop.OrderManagementService.Entities.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler
        : IRequestHandler<GetOrderListQuery, IList<OrderLookupDto>>
    {
        readonly IOrderDbContext _context;
        readonly IMapper _mapper;

        public GetOrderListQueryHandler
            (IOrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<OrderLookupDto>> Handle(GetOrderListQuery request, 
            CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Where(x => (request.UserId == null || x.UserId == request.UserId)
                    && (request.RecipientFirstName == null 
                        || x.RecipientFirstName == request.RecipientFirstName)
                    && (request.RecipientLastName == null 
                        || x.RecipientLastName == request.RecipientLastName)
                    && (request.RecipientEmail == null 
                        || x.RecipientEmail == request.RecipientEmail)
                    && (request.RecipientPhone == null 
                        || x.RecipientPhone == request.RecipientPhone))
                .ProjectTo<OrderLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return orders;
        }
    }
}
