using Application.Dto.Dish;
using FluentValidation;

namespace Infrastructure.Validations.Dish;
public class NewDishDtoValidator : AbstractValidator<NewDishDto>
{
    public NewDishDtoValidator()
    {
        
    }
}
