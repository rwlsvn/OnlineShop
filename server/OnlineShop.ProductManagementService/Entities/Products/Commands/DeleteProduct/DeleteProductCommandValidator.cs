using FluentValidation;

namespace OnlineShop.ProductManagementService.Entities.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator
        : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(deleteProductComamnd =>  deleteProductComamnd.Id)
                .NotEmpty();
        }
    }
}
