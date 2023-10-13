using MediatR;

namespace OnlineShop.ProductManagementService.Entities.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
