using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Validations.Address;
public class AddressConfiguration : IEntityTypeConfiguration<Core.Entities.Address>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Address> builder)
    {
        builder.HasOne(a => a.Restaurant)
            .WithOne(r => r.Address)
            .HasForeignKey<Core.Entities.Address>(a => a.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

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
