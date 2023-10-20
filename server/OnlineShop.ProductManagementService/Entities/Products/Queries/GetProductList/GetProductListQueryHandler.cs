using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductManagementService.Data;
using System.Linq;

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
                .Where(x => (request.CategotyName == null 
                        || x.Category.Name == request.CategotyName)
                    && (request.MinPrice == null 
                        || x.Price >= request.MinPrice)
                    && (request.MaxPrice == null 
                        || x.Price <= request.MaxPrice)
                    && (request.Name == null 
                        || x.Name.ToLower().Contains(request.Name.ToLower())))
                .Skip((request.Page - 1) * 25)
                .Take(25)
                .ProjectTo<ProductLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }
    }
}
