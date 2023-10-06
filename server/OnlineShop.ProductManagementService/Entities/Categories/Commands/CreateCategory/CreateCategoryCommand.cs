using MediatR;

namespace OnlineShop.ProductManagementService.Entities.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
