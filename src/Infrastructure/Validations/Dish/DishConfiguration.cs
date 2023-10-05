using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Validations.Dish;

public class DishConfiguration : IEntityTypeConfiguration<Core.Entities.Dish>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Dish> builder)
    {
        builder.HasOne(d => d.Restaurant)
            .WithMany(r => r.Dishes)
            .HasForeignKey(d => d.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Price)
            .IsRequired()
            .HasPrecision(6, 2);
    }
}