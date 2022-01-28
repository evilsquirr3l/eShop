using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(
            new UserRole()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new UserRole()
            {
                Id = 2,
                Name = "Moderator",
                NormalizedName = "MODERATOR"
            },
            new UserRole()
            {
                Id = 3,
                Name = "User",
                NormalizedName = "USER"
            }
        );
    }
}