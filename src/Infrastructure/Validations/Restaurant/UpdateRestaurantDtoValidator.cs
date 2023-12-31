﻿using Application.Models.Dto.Restaurant;
using FluentValidation;

namespace Infrastructure.Validations.Restaurant;

public class UpdateRestaurantDtoValidator : AbstractValidator<UpdateRestaurantDto>
{
    public UpdateRestaurantDtoValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Restaurant name is required.")
            .MaximumLength(50).WithMessage("Restaurant name can't be longer than 50 characters.");

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage("Restaurant description is required.")
            .MaximumLength(200).WithMessage("Restaurant description can't be longer than 200 characters.");
    }
}