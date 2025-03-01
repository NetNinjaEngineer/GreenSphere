using FluentValidation;

namespace GreenSphere.Application.Features.Orders.Commands.CreateOnlineOrder;

public sealed class CreateOnlineOrderCommandValidator : AbstractValidator<CreateOnlineOrderCommand>
{
    public CreateOnlineOrderCommandValidator()
    {
        RuleFor(c => c.PaymentIntentId)
            .NotNull().WithMessage("{PropertyName} can not be null.")
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}