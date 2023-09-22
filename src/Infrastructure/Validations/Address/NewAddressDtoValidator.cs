using Application.Dto.Address;
using FluentValidation;

namespace Infrastructure.Validations.Address;
public class NewAddressDtoValidator : AbstractValidator<NewAddressDto>
{
    public NewAddressDtoValidator()
    {
        
    }
}
