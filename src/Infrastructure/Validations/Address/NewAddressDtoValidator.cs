﻿using Application.Dto.Address;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Infrastructure.Validations.Address;
public class NewAddressDtoValidator : AbstractValidator<NewAddressDto>
{
    public NewAddressDtoValidator()
    {
        RuleFor(a => a.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City can't be longer than 50 characters.");

        RuleFor(a => a.Street)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City can't be longer than 50 characters.");

        RuleFor(a => a.PostalCode)
            .Matches(new Regex("\\d{2}-\\d{3}"))
            .WithMessage("Invalid postal code format.");
    }
}
