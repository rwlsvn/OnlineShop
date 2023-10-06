using MediatR;

namespace OnlineShop.ProductManagementService.Entities.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
