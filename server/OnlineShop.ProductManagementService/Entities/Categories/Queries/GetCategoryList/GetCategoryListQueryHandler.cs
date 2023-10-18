using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductManagementService.Data;

namespace OnlineShop.ProductManagementService.Entities.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQueryHandler
        : IRequestHandler<GetCategoryListQuery, IList<CategoryLookupDto>>
    {
        readonly IProductsDbContext _context;
        readonly IMapper _mapper;

        public GetCategoryListQueryHandler
            (IProductsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<CategoryLookupDto>> Handle(GetCategoryListQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Categories
                .ProjectTo<CategoryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
