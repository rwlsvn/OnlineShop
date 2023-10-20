using FluentValidation;

namespace OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductList
{
    public class GetProductListQueryValidator
         : AbstractValidator<GetProductListQuery>
    {
        public GetProductListQueryValidator()
        {
            RuleFor(getProductListQuery => getProductListQuery.Page)
                .GreaterThan(0);
        }
    }
}
