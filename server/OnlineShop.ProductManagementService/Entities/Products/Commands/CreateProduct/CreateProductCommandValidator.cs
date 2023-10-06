using FluentValidation;

namespace OnlineShop.ProductManagementService.Entities.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator 
        : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(createProductCommand => createProductCommand.CategoryId)
                .NotEmpty();
            RuleFor(createProductCommand => createProductCommand.Name)
               .NotEmpty()
               .MaximumLength(256);
            RuleFor(createProductCommand => createProductCommand.Description)
               .NotEmpty()
               .MaximumLength(1024);
            RuleFor(createProductCommand => createProductCommand.Price)
               .PrecisionScale(18, 2, false);
            RuleFor(createProductCommand => createProductCommand.Image)
                .NotEmpty();
        }
    }
}
