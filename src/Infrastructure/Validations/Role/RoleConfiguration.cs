using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Validations.Role;
public class RoleConfiguration : IEntityTypeConfiguration<Core.Entities.Role>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Role> builder)
    {
        builder.HasOne(r => r.User)
            .WithOne(u => u.Role)
            .HasForeignKey<Core.Entities.Role>(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
