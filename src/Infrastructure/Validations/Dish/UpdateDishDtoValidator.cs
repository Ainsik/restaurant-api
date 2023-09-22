using Application.Dto.Dish;
using FluentValidation;

namespace Infrastructure.Validations.Dish;
public class UpdateDishDtoValidator : AbstractValidator<UpdateDishDto>
{
    public UpdateDishDtoValidator()
    {
        
    }
}
