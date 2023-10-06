using System.Text.RegularExpressions;
using Application.Models.Dto.Restaurant;
using FluentValidation;
using Infrastructure.DbContext;

namespace Infrastructure.Validations.Restaurant;

public class NewRestaurantDtoValidator : AbstractValidator<NewRestaurantDto>
{
    public NewRestaurantDtoValidator(RestaurantDbContext dbContext)
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Restaurant name is required.")
            .MaximumLength(50).WithMessage("Restaurant name can't be longer than 50 characters.");

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage("Restaurant description is required.")
            .MaximumLength(200).WithMessage("Restaurant description can't be longer than 200 characters.");

        RuleFor(r => r.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category can't be longer than 50 characters.");

        RuleFor(r => r.HasDelivery)
            .NotEmpty().WithMessage("Has delivery is required.");

        RuleFor(r => r.ContactEmail)
            .NotEmpty().WithMessage("Restaurant e-mail is required.")
            .MaximumLength(50).WithMessage("Restaurant e-mail can't be longer than 50 characters.")
            .EmailAddress().WithMessage("Not valid e-mail address.")
            .Custom((value, context) =>
            {
                var emailInUse = dbContext.Restaurants.Any(d => d.ContactEmail == value);
                if (emailInUse) context.AddFailure("ContactEmail", "That email is taken.");
            });

        RuleFor(r => r.ContactNumber)
            .NotEmpty().WithMessage("Restaurant phone number  is required.")
            .Length(9).WithMessage("Restaurant phone number must have exactly 9 characters.");

        RuleFor(r => r.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City can't be longer than 50 characters.");

        RuleFor(r => r.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(50).WithMessage("Street can't be longer than 50 characters.");

        RuleFor(r => r.PostalCode)
            .Matches(new Regex("\\d{2}-\\d{3}"))
            .WithMessage("Invalid postal code format.");
    }
}