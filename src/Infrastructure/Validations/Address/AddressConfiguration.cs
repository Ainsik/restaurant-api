using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Validations.Address;
public class AddressConfiguration : IEntityTypeConfiguration<Core.Entities.Address>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Address> builder)
    {
        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.PostalCode)
            .IsRequired()
            .HasMaxLength(6)
            .IsFixedLength();
    }
}
