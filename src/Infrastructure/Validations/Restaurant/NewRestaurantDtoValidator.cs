using Application.Dto.Restaurant;
using FluentValidation;
using Infrastructure.DbContext;

namespace Infrastructure.Validations.Restaurant;
public class NewRestaurantDtoValidator : AbstractValidator<NewRestaurantDto>
{
    public NewRestaurantDtoValidator(RestaurantDbContext dbContext)
    {
        
    }
}
