using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Validations.Restaurant;
public class RestaurantConfiguration : IEntityTypeConfiguration<Core.Entities.Restaurant>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Restaurant> builder)
    {
        throw new NotImplementedException();
    }
}
