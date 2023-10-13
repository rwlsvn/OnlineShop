using MediatR;

namespace OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductList
{
    public record GetProductListQuery : IRequest<IList<ProductLookupDto>>;
}
