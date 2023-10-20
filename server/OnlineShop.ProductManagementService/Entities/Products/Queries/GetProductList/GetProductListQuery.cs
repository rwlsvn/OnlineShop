using MediatR;

namespace OnlineShop.ProductManagementService.Entities.Products.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<IList<ProductLookupDto>>
    {
        public string? CategotyName { get; set; }
        public string? Name { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int Page { get; set; } = 1;
    }
}
