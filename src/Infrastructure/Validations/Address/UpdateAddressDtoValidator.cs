﻿using System.Text.RegularExpressions;
using Application.Dto.Address;
using FluentValidation;

namespace Infrastructure.Validations.Address;
public class UpdateAddressDtoValidator : AbstractValidator<UpdateAddressDto>
{
    public UpdateAddressDtoValidator()
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
