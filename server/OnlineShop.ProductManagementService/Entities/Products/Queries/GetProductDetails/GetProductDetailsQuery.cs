using MediatR;

namespace OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductDetails
{
    public class GetProductDetailsQuery : IRequest<ProductDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
