using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductManagementService.Data;

namespace OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler
        : IRequestHandler<GetProductListQuery, IList<ProductLookupDto>>
    {
        readonly IProductsDbContext _context;
        readonly IMapper _mapper;

        public GetProductListQueryHandler
            (IProductsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<ProductLookupDto>> 
            Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .ProjectTo<ProductLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }
    }
}
