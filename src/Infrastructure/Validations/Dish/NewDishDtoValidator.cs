﻿using Application.Models.Dto.Dish;
using FluentValidation;

namespace Infrastructure.Validations.Dish;

public class NewDishDtoValidator : AbstractValidator<NewDishDto>
{
    public NewDishDtoValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Dish name is required.")
            .MaximumLength(50).WithMessage("Dish name can't be longer than 50 characters.");

        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Dish description is required.")
            .MaximumLength(200).WithMessage("Dish description can't be longer than 200 characters.");

        RuleFor(d => d.Price)
            .NotEmpty().WithMessage("Dish price is required.")
            .PrecisionScale(6, 2, true);
    }
}