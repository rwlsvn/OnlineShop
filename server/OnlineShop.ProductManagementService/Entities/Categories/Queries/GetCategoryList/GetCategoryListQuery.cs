using MediatR;

namespace OnlineShop.ProductManagementService.Entities.Categories.Queries.GetCategoryList
{
    public record GetCategoryListQuery : IRequest<IList<CategoryLookupDto>>;
}
