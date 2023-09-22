using Application.Dto.Address;
using FluentValidation;

namespace Infrastructure.Validations.Address;
public class UpdateAddressDtoValidator : AbstractValidator<UpdateAddressDto>
{
    public UpdateAddressDtoValidator()
    {
        
    }
}
