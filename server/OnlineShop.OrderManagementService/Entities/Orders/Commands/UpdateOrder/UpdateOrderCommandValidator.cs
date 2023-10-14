using FluentValidation;

namespace OnlineShop.OrderManagementService.Entities.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator
        : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(updateOrderCommand => updateOrderCommand.Id)
                .NotEmpty();
            RuleFor(updateOrderCommand => updateOrderCommand.Id)
                .NotEmpty();
            RuleFor(createOrderCommand => createOrderCommand.RecipientFirstName)
                .NotEmpty()
                .MaximumLength(24);
            RuleFor(createOrderCommand => createOrderCommand.RecipientLastName)
                .NotEmpty()
                .MaximumLength(24);
            RuleFor(createOrderCommand => createOrderCommand.RecipientEmail)
                .MaximumLength(36);
            RuleFor(createOrderCommand => createOrderCommand.RecipientPhone)
                .NotEmpty()
                .MaximumLength(16);
            RuleFor(createOrderCommand => createOrderCommand.Country)
                .NotEmpty()
                .MaximumLength(36);
            RuleFor(createOrderCommand => createOrderCommand.City)
                .NotEmpty()
                .MaximumLength(36);
            RuleFor(createOrderCommand => createOrderCommand.StreetAddresss)
                .NotEmpty()
                .MaximumLength(64);
            RuleFor(createOrderCommand => createOrderCommand.PostalCode)
                .NotEmpty()
                .MaximumLength(12);
        }
    }
}
