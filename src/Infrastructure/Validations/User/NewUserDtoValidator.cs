using System.Text.RegularExpressions;
using Application.Dto.User;
using FluentValidation;
using Infrastructure.DbContext;

namespace Infrastructure.Validations.User;
public class NewUserDtoValidator : AbstractValidator<NewUserDto>
{
    public NewUserDtoValidator(RestaurantDbContext dbContext)
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Restaurant e-mail is required.")
            .MaximumLength(50).WithMessage("Restaurant e-mail can't be longer than 50 characters.")
            .EmailAddress().WithMessage("Not valid e-mail address.")
            .Custom((value, context) =>
            {
                var emailInUse = dbContext.Restaurants.Any(d => d.ContactEmail == value);
                if (emailInUse) context.AddFailure("ContactEmail", "That email is taken.");
            });

        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("Restaurant name is required.")
            .MaximumLength(50).WithMessage("Restaurant name can't be longer than 50 characters.");

        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("Restaurant name is required.")
            .MaximumLength(50).WithMessage("Restaurant name can't be longer than 50 characters.");

        RuleFor(r => r.Nationality)
            .NotEmpty().WithMessage("Restaurant name is required.")
            .MaximumLength(50).WithMessage("Restaurant name can't be longer than 50 characters.");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Restaurant name is required.")
            .Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,50}$"))
            .WithMessage("Minimum 8 and maximum 50 characters, at least one uppercase letter, one lowercase letter, one number and one special character.");
    }
}
