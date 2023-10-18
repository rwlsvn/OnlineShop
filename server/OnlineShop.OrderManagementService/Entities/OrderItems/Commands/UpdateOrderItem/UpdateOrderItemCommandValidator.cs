using FluentValidation;

namespace OnlineShop.OrderManagementService.Entities.OrderItems.Commands.UpdateOrderItem
{
    public class UpdateOrderItemCommandValidator
        : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemCommandValidator()
        {
            RuleFor(createOrderItemCommand => createOrderItemCommand.Id)
                .NotEmpty();
            RuleFor(createOrderItemCommand => createOrderItemCommand.ProductId)
                .NotEmpty();
            RuleFor(createOrderItemCommand => createOrderItemCommand.ProductName)
                .NotEmpty()
                .MaximumLength(256);
            RuleFor(createOrderItemCommand => createOrderItemCommand.ProductPrice)
                .NotEmpty()
                .PrecisionScale(18, 2, true);
        }
    }
}
