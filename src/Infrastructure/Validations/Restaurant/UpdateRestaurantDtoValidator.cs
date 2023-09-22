using Application.Dto.Restaurant;
using FluentValidation;
using Infrastructure.DbContext;

namespace Infrastructure.Validations.Restaurant;
public class UpdateRestaurantDtoValidator : AbstractValidator<UpdateRestaurantDto>
{
    public UpdateRestaurantDtoValidator(RestaurantDbContext dbContext)
    {
       
    }
}
