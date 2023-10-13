using FluentValidation;

namespace OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQueryValidator
        : AbstractValidator<GetProductDetailsQuery>
    {
        public GetProductDetailsQueryValidator()
        {
            RuleFor(getProductDetailsQuery => getProductDetailsQuery.Id)
                .NotEmpty();
        }
    }
}
