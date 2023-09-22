using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Validations.Dish;
public class DishConfiguration : IEntityTypeConfiguration<Core.Entities.Dish>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Dish> builder)
    {
        throw new NotImplementedException();
    }
}
