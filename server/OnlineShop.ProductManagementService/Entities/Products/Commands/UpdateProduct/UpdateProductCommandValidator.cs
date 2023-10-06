using FluentValidation;

namespace OnlineShop.ProductManagementService.Entities.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator
        : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(updateProductCommand => updateProductCommand.Id)
                .NotEmpty();
            RuleFor(updateProductCommand => updateProductCommand.CategoryId)
                .NotEmpty();
            RuleFor(updateProductCommand => updateProductCommand.Name)
               .NotEmpty()
               .MaximumLength(256);
            RuleFor(updateProductCommand => updateProductCommand.Description)
               .NotEmpty()
               .MaximumLength(1024);
            RuleFor(updateProductCommand => updateProductCommand.Price)
               .PrecisionScale(18, 2, false);
        }
    }
}
