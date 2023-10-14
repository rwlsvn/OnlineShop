using FluentValidation;

namespace OnlineShop.OrderManagementService.Entities.OrderItems.Commands.DeleteOrderItem
{
    public class DeleteOrderItemCommandValidator
        : AbstractValidator<DeleteOrderItemCommand>
    {
        public DeleteOrderItemCommandValidator()
        {
            RuleFor(deleteOrderItemCommand => deleteOrderItemCommand.Id)
                .NotEmpty();
        }
    }
}
