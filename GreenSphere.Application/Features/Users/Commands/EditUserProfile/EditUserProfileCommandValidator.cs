using FluentValidation;

namespace GreenSphere.Application.Features.Users.Commands.EditUserProfile;

public sealed class EditUserProfileCommandValidator : AbstractValidator<EditUserProfileCommand>
{
    public EditUserProfileCommandValidator()
    {
        RuleFor(c => c.Email)
             .NotNull().WithMessage("Email cannot be null")
             .NotEmpty().WithMessage("Email is required")
             .EmailAddress().WithMessage("Email is not in a valid format");

        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

    }
}