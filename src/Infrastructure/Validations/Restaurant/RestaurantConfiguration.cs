using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Validations.Restaurant;

public class RestaurantConfiguration : IEntityTypeConfiguration<Core.Entities.Restaurant>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Restaurant> builder)
    {
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.HasDelivery)
            .IsRequired();

        builder.Property(r => r.ContactEmail)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(r => r.ContactEmail)
            .IsUnique();

        builder.Property(r => r.ContactNumber)
            .IsRequired()
            .HasMaxLength(9)
            .IsFixedLength();
    }
}