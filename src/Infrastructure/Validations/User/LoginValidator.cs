using System.Text.RegularExpressions;
using Application.Dto.User;
using FluentValidation;

namespace Infrastructure.Validations.User;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("User e-mail is required.")
            .MaximumLength(50).WithMessage("User e-mail can't be longer than 50 characters.")
            .EmailAddress().WithMessage("Not valid e-mail address.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("User password is required.")
            .Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,50}$"))
            .WithMessage(
                "Minimum 8 and maximum 50 characters, at least one uppercase letter, one lowercase letter, one number and one special character.");
    }
}