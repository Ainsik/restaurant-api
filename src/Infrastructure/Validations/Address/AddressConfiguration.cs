using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Validations.Address;
public class AddressConfiguration : IEntityTypeConfiguration<Core.Entities.Address>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Address> builder)
    {
        throw new NotImplementedException();
    }
}
