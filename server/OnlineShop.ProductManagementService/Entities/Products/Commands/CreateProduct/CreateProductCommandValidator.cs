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
               .MaximumLength(1024);
            RuleFor(createProductCommand => createProductCommand.Price)
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
