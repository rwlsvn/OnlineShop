using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductManagementService.Data;

namespace OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQueryHandler
        : IRequestHandler<GetProductDetailsQuery, ProductDetailsVm>
    {
        readonly IProductsDbContext _context;
        readonly IMapper _mapper;

        public GetProductDetailsQueryHandler
            (IProductsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailsVm> Handle(GetProductDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var product = await _context.Products.Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<ProductDetailsVm>(product);
        }
    }
}
