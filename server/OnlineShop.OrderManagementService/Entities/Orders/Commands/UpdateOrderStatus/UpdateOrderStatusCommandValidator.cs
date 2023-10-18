using FluentValidation;

namespace OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandValidator
        : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(updateOrderStatusCommand => updateOrderStatusCommand.Id)
                .NotNull();
            RuleFor(updateOrderStatusCommand => updateOrderStatusCommand.Status)
                .NotNull();
        }
    }
}
