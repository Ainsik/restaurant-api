using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Validations.Dish;
public class DishConfiguration : IEntityTypeConfiguration<Core.Entities.Dish>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Dish> builder)
    {
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Price)
            .IsRequired();
    }
}
