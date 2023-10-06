using FluentValidation;

namespace OnlineShop.ProductManagementService.Entities.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator 
        : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(updateCategoryComamnd => updateCategoryComamnd.Id).NotEmpty();
            RuleFor(updateCategoryComamnd => updateCategoryComamnd.Name)
                .NotEmpty()
                .MaximumLength(64);
        }
    }
}
