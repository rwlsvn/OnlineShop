using FluentValidation;

namespace OnlineShop.ProductManagementService.Entities.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator 
        : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(createCategoryCommand => createCategoryCommand.Name)
                .NotEmpty()
                .MaximumLength(64);
        }
    }
}
