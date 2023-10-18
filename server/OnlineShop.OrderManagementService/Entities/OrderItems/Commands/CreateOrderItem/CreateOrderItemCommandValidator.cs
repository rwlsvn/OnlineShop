using FluentValidation;

namespace OnlineShop.OrderManagementService.Entities.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommandValidator
        : AbstractValidator<CreateOrderItemCommand>
    {
        public CreateOrderItemCommandValidator()
        {
            RuleFor(createOrderItemCommand => createOrderItemCommand.OrderId)
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
