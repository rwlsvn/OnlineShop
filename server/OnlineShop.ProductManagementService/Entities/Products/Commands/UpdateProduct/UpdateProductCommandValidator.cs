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
               .MaximumLength(1024);
            RuleFor(updateProductCommand => updateProductCommand.Price)
               .PrecisionScale(18, 2, false);
            RuleFor(createProductCommand => createProductCommand.Image)
                .Must(image =>
                {
                    if (image == null)
                        return true;

                    string[] allowedExtensions = { ".jpg", ".png" };
                    string fileExtension = Path.GetExtension(image.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                        return false;

                    long maxFileSize = 5 * 1024 * 1024;

                    if (image.Length > maxFileSize)
                        return false;

                    return true;
                })
                .WithMessage("Invalid file format or size.");
        }
    }
}
