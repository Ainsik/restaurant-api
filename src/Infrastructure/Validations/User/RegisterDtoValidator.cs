using System.Text.RegularExpressions;
using Application.Dto.User;
using FluentValidation;
using Infrastructure.DbContext;

namespace Infrastructure.Validations.User;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator(RestaurantDbContext dbContext)
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("User e-mail is required.")
            .MaximumLength(50).WithMessage("User e-mail can't be longer than 50 characters.")
            .EmailAddress().WithMessage("Not valid e-mail address.")
            .Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if (emailInUse) context.AddFailure("Email", "That email is taken.");
            });

        RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("User first name is required.")
            .MaximumLength(50).WithMessage("User first name can't be longer than 50 characters.");

        RuleFor(u => u.LastName)
            .NotEmpty().WithMessage("User last name is required.")
            .MaximumLength(50).WithMessage("User last name can't be longer than 50 characters.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("User password is required.")
            .Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,50}$"))
            .WithMessage(
                "Minimum 8 and maximum 50 characters, at least one uppercase letter, one lowercase letter, one number and one special character.");
    }
}