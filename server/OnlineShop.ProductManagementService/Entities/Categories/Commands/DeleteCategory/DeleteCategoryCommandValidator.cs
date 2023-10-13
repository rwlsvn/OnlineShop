using FluentValidation;

namespace OnlineShop.ProductManagementService.Entities.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator 
        : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(deleteCategoryCommand => deleteCategoryCommand.Id).NotEmpty();
        }
    }
}
